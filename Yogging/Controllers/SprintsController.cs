using System;
using System.Web.Mvc;
using Yogging.Helpers;
using Yogging.Sprints;
using Yogging.ViewModels;

namespace Yogging.Controllers
{
    [Authorize]
    public class SprintsController : Controller
    {
        private readonly ISprintService _sprintService;

        public SprintsController(ISprintService sprintService)
        {
            _sprintService = sprintService;
        }

        // GET: Sprints
        public ActionResult Index()
        {
            var sprints = _sprintService.GetActive();

            return View(sprints);
        }

        public ActionResult Finished()
        {
            var sprints = _sprintService.GetClosed();

            return View(sprints);
        }

        // GET: Sprints/Details/5
        public ActionResult Details(Guid id)
        {
            var sprint = _sprintService.GetById(id);

            if (sprint == null)
            {
                return HttpNotFound();
            }

            return View(sprint);
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
        public ActionResult Create(SprintViewModel sprint)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _sprintService.Create(sprint);
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
        public ActionResult Edit(Guid id)
        {
            var sprint = _sprintService.GetById(id);
            if (sprint == null)
            {
                return HttpNotFound();
            }

            return View(sprint);
        }

        // POST: Sprints/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(SprintViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _sprintService.Update(viewModel);
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
        public ActionResult Delete(Guid id)
        {
            var sprint = _sprintService.GetById(id);
            if (sprint == null)
            {
                return HttpNotFound();
            }
            return View(sprint);
        }

        // POST: Sprints/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                var sprint = _sprintService.GetById(id);
                if (sprint != null)
                {
                    _sprintService.Delete(sprint);
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
    }
}
