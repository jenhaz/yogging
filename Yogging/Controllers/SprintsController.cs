using System;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;
using Yogging.DAL.Context;
using Yogging.Domain.Sprints;
using Yogging.Helpers;
using Yogging.Sprints;
using Yogging.ViewModels;

namespace Yogging.Controllers
{
    [Authorize]
    public class SprintsController : Controller
    {
        private readonly ISprintService _sprintService;
        private readonly YoggingContext _db;

        public SprintsController(
            ISprintService sprintService, 
            YoggingContext db)
        {
            _sprintService = sprintService;
            _db = db;
        }

        // GET: Sprints
        public ActionResult Index()
        {
            var sprints = _sprintService.GetAllActiveSprints();

            return View(sprints);
        }

        public ActionResult Finished()
        {
            var sprints = _sprintService.GetAllClosedSprints();

            return View(sprints);
        }

        // GET: Sprints/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            var sprint = _db.Sprints.Find(id);
            if (sprint == null)
            {
                return HttpNotFound();
            }

            var viewModel = _sprintService.GetSprint(sprint);

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
                try
                {
                    _db.Sprints.Add(sprint);
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ExceptionHelper.LogException(e, "Error creating new sprint");
                    ViewBag.ErrorMessage = "Error creating new sprint";
                    return View("~/Views/Shared/Error.cshtml");
                }
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
            var sprint = _db.Sprints.Find(id);
            if (sprint == null)
            {
                return HttpNotFound();
            }

            var viewModel = _sprintService.GetSprint(sprint);

            return View(viewModel);
        }

        // POST: Sprints/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
         public ActionResult Edit(SprintViewModel viewModel)
        {
            var sprint = _sprintService.PutSprint(viewModel);

            if (ModelState.IsValid)
            {
                try
                {
                    _db.Entry(sprint).State = EntityState.Modified;
                    _db.SaveChanges();
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ExceptionHelper.LogException(e, "Error editing sprint " + viewModel.Name + " - " + viewModel.Id);
                    ViewBag.ErrorMessage = "Error updating sprint " + viewModel.Name;
                    return View("~/Views/Shared/Error.cshtml");
                }
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
            var sprint = _db.Sprints.Find(id);
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
            try
            {
                var sprint = _db.Sprints.Find(id);
                if (sprint != null)
                {
                    _db.Sprints.Remove(sprint);
                    _db.SaveChanges();
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ExceptionHelper.LogException(e, "Error deleting sprint " + id);
                ViewBag.ErrorMessage = "Error deleting sprint";
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
