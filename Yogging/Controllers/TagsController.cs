using System;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using Yogging.DAL.Context;
using Yogging.Models;
using Yogging.Services.Helpers;
using Yogging.Services.Interfaces;

namespace Yogging.Controllers
{
    [Authorize]
    public class TagsController : Controller
    {
        private readonly ITagService _tagService;
        private readonly YoggingContext _db;

        public TagsController(
            ITagService tagService, 
            YoggingContext db)
        {
            _tagService = tagService;
            _db = db;
        }

        // GET: Tags
        public ActionResult Index()
        {
            var tags = _tagService.GetAllTags();

            return View(tags);
        }

        // GET: Tags/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == Guid.Empty || id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tag = _tagService.GetTagById(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        // GET: Tags/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tags/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,Colour")] Tag tag)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _db.Tags.Add(tag);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ExceptionHelper.LogException(e, "Error creating new tag");
                    ViewBag.ErrorMessage = "Error creating new tag";
                    return View("~/Views/Shared/Error.cshtml");
                }
            }

            return View(tag);
        }

        // GET: Tags/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tag = _db.Tags.Find(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        // POST: Tags/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,Colour")] Tag tag)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _db.Entry(tag).State = EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ExceptionHelper.LogException(e, "Error editing tag " + tag.Id + " - " + tag.Name);
                    ViewBag.ErrorMessage = "Error updating tag " + tag.Name;
                    return View("~/Views/Shared/Error.cshtml");
                }
            }
            return View(tag);
        }

        // GET: Tags/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var tag = _db.Tags.Find(id);
            if (tag == null)
            {
                return HttpNotFound();
            }
            return View(tag);
        }

        // POST: Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                var tag = _db.Tags.Find(id);
                if (tag != null)
                {
                    _db.Tags.Remove(tag);
                    _db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ExceptionHelper.LogException(e, "Error deleting tag " + id);
                ViewBag.ErrorMessage = "Error deleting tag";
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
