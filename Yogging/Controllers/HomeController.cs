using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using Yogging.DAL.Context;
using Yogging.Models;
using Yogging.Models.ViewModels;
using Yogging.Services.Helpers;
using Yogging.Services.Interfaces;

namespace Yogging.Controllers
{
    public class HomeController : Controller
    {
        private YoggingContext db = new YoggingContext();
        private IUserService UserService { get; }
        private ISpotifyService SpotifyService { get; }

        public HomeController(IUserService userService, ISpotifyService spotifyService)
        {
            UserService = userService;
            SpotifyService = spotifyService;
        }

        public ActionResult Index()
        {
            ViewBag.IsHome = true;
            return View();
        }

        public ActionResult About()
        {
            IEnumerable<ProfileViewModel> profiles = UserService.GetAllProfiles();

            return View(profiles);
        }

        public ActionResult Playlists()
        {
            IEnumerable<SpotifyPlaylistViewModel> playlists = SpotifyService.GetAllPlaylists();

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
                    db.Profiles.Add(profile);
                    db.SaveChanges();
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
            Profile profile = db.Profiles.Find(id);
            if (profile == null)
            {
                return HttpNotFound();
            }

            ProfileViewModel viewModel = UserService.GetProfile(profile);

            return View(viewModel);
        }

        // POST: Home/EditProfile/5
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(ProfileViewModel viewModel)
        {
            Profile profile = UserService.PutProfile(viewModel);

            if (ModelState.IsValid)
            {
                try
                {
                    db.Entry(profile).State = EntityState.Modified;
                    db.SaveChanges();
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