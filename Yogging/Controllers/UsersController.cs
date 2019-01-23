using System;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using Yogging.DAL.Context;
using Yogging.Domain.Users;
using Yogging.Helpers;
using Yogging.Users;

namespace Yogging.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;
        private readonly YoggingContext _db;

        public UsersController(
            IUserService userService, 
            YoggingContext db)
        {
            _userService = userService;
            _db = db;
        }

        // GET: Users
        public ActionResult Index()
        {
            var users = _userService.GetAllActiveUsers();

            return View(users);
        }

        // GET: Users/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = _db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,FirstName,LastName,EmailAddress,IsInactive")] User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _db.Users.Add(user);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ExceptionHelper.LogException(e, "Error creating new user");
                    ViewBag.ErrorMessage = "Error creating new user";
                    return View("~/Views/Shared/Error.cshtml");
                }
            }

            return View(user);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = _db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstName,LastName,EmailAddress,IsInactive")] User user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _db.Entry(user).State = EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ExceptionHelper.LogException(e, "Error editing user " + user.Id + " - " + user.FirstName + " " + user.LastName);
                    ViewBag.ErrorMessage = "Error updating user " + user.FirstName + " " + user.LastName;
                    return View("~/Views/Shared/Error.cshtml");
                }
            }
            return View(user);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var user = _db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var user = _db.Users.Find(id);
                if (user != null)
                {
                    _db.Users.Remove(user);
                    _db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ExceptionHelper.LogException(e, "Error deleting user " + id);
                ViewBag.ErrorMessage = "Error deleting user";
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                _db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
