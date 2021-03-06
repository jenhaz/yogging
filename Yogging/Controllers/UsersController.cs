﻿using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Yogging.Helpers;
using Yogging.Services.Users;
using Yogging.ViewModels;

namespace Yogging.Controllers
{
    [Authorize]
    public class UsersController : Controller
    {
        private readonly IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        // GET: Users
        public async Task<ActionResult> Index()
        {
            var users = await _userService.GetActive();

            return View(users);
        }

        // GET: Users/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var user = await _userService.GetById(id);

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
        public async Task<ActionResult> Create(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _userService.Create(user);
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
        public async Task<ActionResult> Edit(Guid id)
        {
            var user = await _userService.GetById(id);

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
        public async Task<ActionResult> Edit(UserViewModel user)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _userService.Update(user);
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
        public async Task<ActionResult> Delete(Guid id)
        {
            var user = await _userService.GetById(id);

            if (user == null)
            {
                return HttpNotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                var user = await _userService.GetById(id);
                if (user != null)
                {
                    await _userService.Delete(user);
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
    }
}
