using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using Yogging.DAL.Context;
using Yogging.Models;
using Yogging.Models.ViewModels;
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

        [Authorize]
        // GET: Home/CreateProfile
        public ActionResult CreateProfile()
        {
            return View();
        }

        [Authorize]
        // POST: Home/CreateProfile
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateProfile([Bind(Include = "Id,FullName,ImageUrl,Blurb,LongerBlurb,InstagramUsername,LinkedInUsername,TwitterUsername,BlogUrl,CurrentJobTitle,ContactEmailAddress")] Profile profile)
        {
            if (ModelState.IsValid)
            {
                db.Profiles.Add(profile);
                db.SaveChanges();
                return RedirectToAction("About");
            }

            return View(profile);
        }

        [Authorize]
        // GET: Home/EditProfile/5
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

        [Authorize]
        // POST: Home/EditProfile/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(ProfileViewModel viewModel)
        {
            Profile profile = UserService.PutProfile(viewModel);

            if (ModelState.IsValid)
            {
                db.Entry(profile).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("About");
            }
            return View(viewModel);
        }
    }
}