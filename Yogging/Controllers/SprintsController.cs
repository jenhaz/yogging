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
    [Authorize]
    public class SprintsController : Controller
    {
        private YoggingContext db = new YoggingContext();
        private IStoryService StoryService { get; }
        private ISprintService SprintService { get; }

        public SprintsController(IStoryService storyService, ISprintService sprintService)
        {
            StoryService = storyService;
            SprintService = sprintService;
        }

        // GET: Sprints
        public ActionResult Index()
        {
            IEnumerable<SprintViewModel> sprints = SprintService.GetAllActiveSprints();

            return View(sprints);
        }

        public ActionResult Finished()
        {
            IEnumerable<SprintViewModel> sprints = SprintService.GetAllExpiredSprints();

            return View(sprints);
        }

        // GET: Sprints/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sprint sprint = db.Sprints.Find(id);
            if (sprint == null)
            {
                return HttpNotFound();
            }

            SprintViewModel viewModel = SprintService.GetSprint(sprint);

            return View(viewModel);
        }

        // GET: Sprints/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Sprints/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Name,StartDate,EndDate,Status")] Sprint sprint)
        {
            if (ModelState.IsValid)
            {
                db.Sprints.Add(sprint);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(sprint);
        }

        // GET: Sprints/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sprint sprint = db.Sprints.Find(id);
            if (sprint == null)
            {
                return HttpNotFound();
            }

            SprintViewModel viewModel = SprintService.GetSprint(sprint);

            return View(viewModel);
        }

        // POST: Sprints/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
         public ActionResult Edit(SprintViewModel viewModel)
        {
            Sprint sprint = SprintService.PutSprint(viewModel);

            if (ModelState.IsValid)
            {
                db.Entry(sprint).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        // GET: Sprints/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sprint sprint = db.Sprints.Find(id);
            if (sprint == null)
            {
                return HttpNotFound();
            }
            return View(sprint);
        }

        // POST: Sprints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sprint sprint = db.Sprints.Find(id);
            db.Sprints.Remove(sprint);
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
