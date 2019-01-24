using System;
using System.Web.Mvc;
using Yogging.Helpers;
using Yogging.Profiles;
using Yogging.Spotify;
using Yogging.ViewModels;

namespace Yogging.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProfileService _profileService;
        private readonly ISpotifyService _spotifyService;

        public HomeController(
            ProfileService profileService, 
            ISpotifyService spotifyService)
        {
            _profileService = profileService;
            _spotifyService = spotifyService;
        }

        public ActionResult Index()
        {
            ViewBag.IsHome = true;

            return View();
        }

        public ActionResult About()
        {
            var profiles = _profileService.GetAll();

            if (profiles == null)
            {
                return View();
            }

            return View(profiles);
        }

        public ActionResult Playlists()
        {
            var playlists = _spotifyService.GetAllPlaylists();

            return View(playlists);
        }
        
        // GET: Home/CreateProfile
        [Authorize]
        public ActionResult CreateProfile()
        {
            return View();
        }

        // POST: Home/CreateProfile
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProfile(ProfileViewModel profile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _profileService.Create(profile);
                    return RedirectToAction("About");
                }
                catch (Exception e)
                {
                    ExceptionHelper.LogException(e, "Error creating new profile");
                    ViewBag.ErrorMessage = "Error creating new profile";
                    return View("~/Views/Shared/Error.cshtml");
                }
            }

            return View(profile);
        }

        // GET: Home/EditProfile/5
        [Authorize]
        public ActionResult EditProfile(Guid id)
        {
            var profile = _profileService.GetById(id);

            if (profile == null)
            {
                return HttpNotFound();
            }

            return View(profile);
        }

        // POST: Home/EditProfile/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(ProfileViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _profileService.Update(viewModel);
                    return RedirectToAction("About");
                }
                catch (Exception e)
                {
                    ExceptionHelper.LogException(e, "Error editing profile for " + viewModel.FullName);
                    ViewBag.ErrorMessage = "Error updating profile for " + viewModel.FullName;
                    return View("~/Views/Shared/Error.cshtml");
                }
            }
            return View(viewModel);
        }
    }
}