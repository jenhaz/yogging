using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Net;
using System.Threading.Tasks;
using System.Web.Mvc;
using Yogging.DAL.Context;
using Yogging.Models;
using Yogging.Models.ViewModels;
using Yogging.Services.Helpers;
using Yogging.Services.Interfaces;

namespace Yogging.Controllers
{
    [Authorize]
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

            StoryViewModel viewModel = StoryService.GetStory(story);

            return View(viewModel);
        }

        public PartialViewResult PartialDetails(int id)
        {
            Story story = db.Stories.Find(id);
            StoryViewModel viewModel = StoryService.GetStory(story);

            return PartialView("_StoriesDetail", viewModel);
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
                try
                {
                    story.CreatedDate = DateTime.Now.ToString();
                    story.LastUpdated = DateTime.Now.ToString();
                    db.Stories.Add(story);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ExceptionHelper.LogException(e, "Error creating new story");
                    ViewBag.ErrorMessage = "Error creating new story";
                    return View("~/Views/Shared/Error.cshtml");
                }
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

            StoryViewModel viewModel = StoryService.GetStory(story);

            if (Request.IsAjaxRequest())
                return PartialView("_EditPartial", viewModel);

            return View(viewModel);
        }

        // POST: Stories/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(StoryViewModel viewModel)
        {
            Story story = StoryService.PutStory(viewModel);

            if (ModelState.IsValid)
            {
                try
                {
                    db.Stories.Attach(story);
                    story.LastUpdated = DateTime.Now.ToString();
                    db.Entry(story).State = EntityState.Modified;
                    db.Entry(story).Property("CreatedDate").IsModified = false;
                    var task = db.SaveChangesAsync();
                    await task;

                    if (Request.IsAjaxRequest())
                    {
                        return Content("success");
                    }

                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ExceptionHelper.LogException(e, "Error editing story " + viewModel.Id + " - " + viewModel.Name);
                    ViewBag.ErrorMessage = "Error updating story " + viewModel.Name;
                    return View("~/Views/Shared/Error.cshtml");
                }
            }

            ViewBag.SprintId = new SelectList(db.Sprints, "Id", "Name", viewModel.SprintId);
            ViewBag.TagId = new SelectList(db.Tags, "Id", "Name", viewModel.TagId);
            ViewBag.UserId = new SelectList(db.Users, "Id", "FirstName", viewModel.UserId);
            return View(viewModel);
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
            try
            {
                Story story = db.Stories.Find(id);
                db.Stories.Remove(story);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ExceptionHelper.LogException(e, "Error deleting story " + id);
                ViewBag.ErrorMessage = "Error deleting story";
                return View("~/Views/Shared/Error.cshtml");
            }
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
