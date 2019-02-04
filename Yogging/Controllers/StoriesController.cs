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
        public async Task<ActionResult> Index()
        {
            var stories = await _storyService.GetAll();

            return View(stories);
        }

        // GET: Stories/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var story = await _storyService.GetById(id);

            if (story == null)
            {
                return HttpNotFound();
            }

            return View(story);
        }

        public async Task<PartialViewResult> PartialDetails(Guid id)
        {
            var story = await _storyService.GetById(id);

            return PartialView("_StoriesDetail", story);
        }

        // GET: Stories/Create
        public async Task<ActionResult> Create()
        {
            ViewBag.SprintId = await GetSprintsSelectList();
            ViewBag.TagId = await GetTagsSelectList();
            ViewBag.UserId = await GetUsersSelectList();

            return View();
        }

        // POST: Stories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(StoryViewModel story)
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

            ViewBag.SprintId = await GetSprintsSelectList(story);
            ViewBag.TagId = await GetTagsSelectList(story);
            ViewBag.UserId = await GetUsersSelectList(story);

            return View(story);
        }
        
        // GET: Stories/Edit/5
        public async Task<ActionResult> Edit(Guid id)
        {
            var story = await _storyService.GetById(id);
            if (story == null)
            {
                return HttpNotFound();
            }

            ViewBag.SprintId = await GetSprintsSelectList(story);
            ViewBag.TagId = await GetTagsSelectList(story);
            ViewBag.UserId = await GetUsersSelectList(story);

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

            ViewBag.SprintId = await GetSprintsSelectList(viewModel);
            ViewBag.TagId = await GetTagsSelectList(viewModel);
            ViewBag.UserId = await GetUsersSelectList(viewModel);

            return View(viewModel);
        }

        // GET: Stories/Delete/5
        public async Task<ActionResult> Delete(Guid id)
        {
            var story = await _storyService.GetById(id);

            if (story == null)
            {
                return HttpNotFound();
            }

            return View(story);
        }

        // POST: Stories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                var story = await _storyService.GetById(id);
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

        private async Task<SelectList> GetSprintsSelectList(StoryViewModel story = null)
        {
            var sprintId = story?.SprintId;
            return new SelectList(await _sprintService.GetAll(), "Id", "Name", sprintId);
        }

        private async Task<SelectList> GetTagsSelectList(StoryViewModel story = null)
        {
            var tagId = story?.TagId;
            return new SelectList(await _tagService.GetAll(), "Id", "Name", tagId);
        }

        private async Task<SelectList> GetUsersSelectList(StoryViewModel story = null)
        {
            var userId = story?.UserId;
            return new SelectList(await _userService.GetActive(), "Id", "FirstName", userId);
        }
    }
}
