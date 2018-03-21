using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web.Mvc;
using Yogging.DAL.Context;
using Yogging.Models;
using Yogging.Models.ViewModels;
using Yogging.Services.Interfaces;

namespace Yogging.Controllers
{
    public class StoriesController : Controller
    {
        private YoggingContext db = new YoggingContext();
        private IStoryService StoryService { get; }

        public StoriesController(IStoryService storyService)
        {
            StoryService = storyService;
        }

        // GET: Stories
        public ActionResult Index()
        {
            IEnumerable<StoryViewModel> stories = StoryService.GetAllStories();

            return View(stories);
        }

        // GET: Stories/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Story story = db.Stories.Find(id);
            if (story == null)
            {
                return HttpNotFound();
            }
            return View(story);
        }

        // GET: Stories/Create
        public ActionResult Create()
        {
            ViewBag.SprintId = new SelectList(db.Sprints, "Id", "Name");
            ViewBag.TagId = new SelectList(db.Tags, "Id", "Name");
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName");
            return View();
        }

        // POST: Stories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,CreatedDate,LastUpdated,Priority,Type,Description,AcceptanceCriteria,Points,Status,UserId,SprintId,TagId")] Story story)
        {
            if (ModelState.IsValid)
            {
                story.CreatedDate = DateTime.Now.ToString();
                story.LastUpdated = DateTime.Now.ToString();
                db.Stories.Add(story);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SprintId = new SelectList(db.Sprints, "Id", "Name", story.SprintId);
            ViewBag.TagId = new SelectList(db.Tags, "Id", "Name", story.TagId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", story.UserId);
            return View(story);
        }

        // GET: Stories/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Story story = db.Stories.Find(id);
            if (story == null)
            {
                return HttpNotFound();
            }
            ViewBag.SprintId = new SelectList(db.Sprints, "Id", "Name", story.SprintId);
            ViewBag.TagId = new SelectList(db.Tags, "Id", "Name", story.TagId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", story.UserId);
            return View(story);
        }

        // POST: Stories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public ActionResult Edit([Bind(Include = "Id,Name,CreatedDate,LastUpdated,Priority,Type,Description,AcceptanceCriteria,Points,Status,UserId,SprintId,TagId")] Story story)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        story.LastUpdated = DateTime.Now.ToString();
        //        db.Entry(story).State = EntityState.Modified;
        //        db.SaveChanges();
        //        return RedirectToAction("Index");
        //    }
            
        //    ViewBag.SprintId = new SelectList(db.Sprints, "Id", "Name", story.SprintId);
        //    ViewBag.TagId = new SelectList(db.Tags, "Id", "Name", story.TagId);
        //    ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", story.UserId);
        //    return View(story);
        //}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Name,CreatedDate,LastUpdated,Priority,Type,Description,AcceptanceCriteria,Points,Status,UserId,SprintId,TagId")] Story story)
        {
            if (ModelState.IsValid)
            {
                story.LastUpdated = DateTime.Now.ToString();
                db.Entry(story).State = EntityState.Modified;
                db.Entry(story).Property("CreatedDate").IsModified = false;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.SprintId = new SelectList(db.Sprints, "Id", "Name", story.SprintId);
            ViewBag.TagId = new SelectList(db.Tags, "Id", "Name", story.TagId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", story.UserId);
            return View(story);
        }

        // GET: Stories/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Story story = db.Stories.Find(id);
            if (story == null)
            {
                return HttpNotFound();
            }
            return View(story);
        }

        // POST: Stories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Story story = db.Stories.Find(id);
            db.Stories.Remove(story);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
