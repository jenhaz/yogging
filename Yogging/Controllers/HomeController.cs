using System;
using System.Threading.Tasks;
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

        public async Task<ActionResult> About()
        {
            var profiles = await _profileService.GetAll();

            if (profiles == null)
            {
                return View();
            }

            return View(profiles);
        }

        public async Task<ActionResult> Playlists()
        {
            var playlists = await _spotifyService.GetAllPlaylists();

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
        public async Task<ActionResult> CreateProfile(ProfileViewModel profile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _profileService.Create(profile);
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
        public async Task<ActionResult> EditProfile(Guid id)
        {
            var profile = await _profileService.GetById(id);

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
        public async Task<ActionResult> EditProfile(ProfileViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _profileService.Update(viewModel);
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