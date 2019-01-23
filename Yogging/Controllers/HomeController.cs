using System;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using Yogging.DAL.Context;
using Yogging.Domain.Profiles;
using Yogging.Helpers;
using Yogging.Profiles;
using Yogging.Spotify;
using Yogging.ViewModels;

namespace Yogging.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProfileService _profileService;
        private readonly ISpotifyService _spotifyService;
        private readonly YoggingContext _db;

        public HomeController(
            IProfileService profileService, 
            ISpotifyService spotifyService, 
            YoggingContext db)
        {
            _profileService = profileService;
            _spotifyService = spotifyService;
            _db = db;
        }

        public ActionResult Index()
        {
            ViewBag.IsHome = true;
            return View();
        }

        public ActionResult About()
        {
            var profiles = _profileService.GetAllProfiles();

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
        public ActionResult CreateProfile([Bind(Include = "Id,FullName,ImageUrl,Blurb,LongerBlurb,InstagramUsername,LinkedInUsername,TwitterUsername,BlogUrl,GitHubUsername,CurrentJobTitle,ContactEmailAddress")] Profile profile)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _db.Profiles.Add(profile);
                    _db.SaveChanges();
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
        public ActionResult EditProfile(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var profile = _db.Profiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }

            var viewModel = _profileService.GetProfile(profile);

            return View(viewModel);
        }

        // POST: Home/EditProfile/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(ProfileViewModel viewModel)
        {
            var profile = _profileService.PutProfile(viewModel);

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Entry(profile).State = EntityState.Modified;
                    _db.SaveChanges();
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