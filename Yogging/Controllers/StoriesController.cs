using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Yogging.Helpers;
using Yogging.Sprints;
using Yogging.Stories;
using Yogging.Tags;
using Yogging.Users;
using Yogging.ViewModels;

namespace Yogging.Controllers
{
    [Authorize]
    public class StoriesController : Controller
    {
        private readonly IStoryService _storyService;
        private readonly ISprintService _sprintService;
        private readonly ITagService _tagService;
        private readonly IUserService _userService;

        public StoriesController(
            IStoryService storyService, 
            ISprintService sprintService, 
            ITagService tagService, 
            IUserService userService)
        {
            _storyService = storyService;
            _sprintService = sprintService;
            _tagService = tagService;
            _userService = userService;
        }

        // GET: Stories
        public ActionResult Index()
        {
            var stories = _storyService.GetAll();

            return View(stories);
        }

        // GET: Stories/Details/5
        public ActionResult Details(Guid id)
        {
            var story = _storyService.GetById(id);

            if (story == null)
            {
                return HttpNotFound();
            }

            return View(story);
        }

        public PartialViewResult PartialDetails(Guid id)
        {
            var story = _storyService.GetById(id);

            return PartialView("_StoriesDetail", story);
        }

        // GET: Stories/Create
        public ActionResult Create()
        {
            ViewBag.SprintId = GetSprintsSelectList();
            ViewBag.TagId = GetTagsSelectList();
            ViewBag.UserId = GetUsersSelectList();

            return View();
        }

        // POST: Stories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(StoryViewModel story)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _storyService.Create(story);
                    return RedirectToAction("Index");
                }
                catch (Exception e)
                {
                    ExceptionHelper.LogException(e, "Error creating new story");
                    ViewBag.ErrorMessage = "Error creating new story";
                    return View("~/Views/Shared/Error.cshtml");
                }
            }

            ViewBag.SprintId = GetSprintsSelectList(story);
            ViewBag.TagId = GetTagsSelectList(story);
            ViewBag.UserId = GetUsersSelectList(story);

            return View(story);
        }
        
        // GET: Stories/Edit/5
        public ActionResult Edit(Guid id)
        {
            var story = _storyService.GetById(id);
            if (story == null)
            {
                return HttpNotFound();
            }

            ViewBag.SprintId = GetSprintsSelectList(story);
            ViewBag.TagId = GetTagsSelectList(story);
            ViewBag.UserId = GetUsersSelectList(story);

            if (Request.IsAjaxRequest())
                return PartialView("_EditPartial", story);

            return View(story);
        }

        // POST: Stories/Edit/5
        [HttpPost]
        public async Task<ActionResult> Edit(StoryViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _storyService.Update(viewModel);

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

            ViewBag.SprintId = GetSprintsSelectList(viewModel);
            ViewBag.TagId = GetTagsSelectList(viewModel);
            ViewBag.UserId = GetUsersSelectList(viewModel);

            return View(viewModel);
        }

        // GET: Stories/Delete/5
        public ActionResult Delete(Guid id)
        {
            var story = _storyService.GetById(id);

            if (story == null)
            {
                return HttpNotFound();
            }

            return View(story);
        }

        // POST: Stories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                var story = _storyService.GetById(id);
                if (story != null)
                {
                    _storyService.Delete(story);
                }
                return RedirectToAction("Index");
            }
            catch (Exception e)
            {
                ExceptionHelper.LogException(e, "Error deleting story " + id);
                ViewBag.ErrorMessage = "Error deleting story";
                return View("~/Views/Shared/Error.cshtml");
            }
        }

        private SelectList GetSprintsSelectList(StoryViewModel story = null)
        {
            var sprintId = story?.SprintId;
            return new SelectList(_sprintService.GetAll(), "Id", "Name", sprintId);
        }

        private SelectList GetTagsSelectList(StoryViewModel story = null)
        {
            var tagId = story?.TagId;
            return new SelectList(_tagService.GetAll(), "Id", "Name", tagId);
        }

        private SelectList GetUsersSelectList(StoryViewModel story = null)
        {
            var userId = story?.UserId;
            return new SelectList(_userService.GetActive(), "Id", "FirstName", userId);
        }
    }
}
