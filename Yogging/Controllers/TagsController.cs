using System;
using System.Threading.Tasks;
using System.Web.Mvc;
using Yogging.Helpers;
using Yogging.Services.Tags;
using Yogging.ViewModels;

namespace Yogging.Controllers
{
    [Authorize]
    public class TagsController : Controller
    {
        private readonly ITagService _tagService;

        public TagsController(ITagService tagService)
        {
            _tagService = tagService;
        }

        // GET: Tags
        public async Task<ActionResult> Index()
        {
            var tags = await _tagService.GetAll();

            return View(tags);
        }

        // GET: Tags/Details/5
        public async Task<ActionResult> Details(Guid id)
        {
            var tag = await _tagService.GetById(id);

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
        public async Task<ActionResult> Create(TagViewModel tag)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _tagService.Create(tag);
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
        public async Task<ActionResult> Edit(Guid id)
        {
            var tag = await _tagService.GetById(id);

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
        public async Task<ActionResult> Edit(TagViewModel tag)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _tagService.Update(tag);
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
        public async Task<ActionResult> Delete(Guid id)
        {
            var tag = await _tagService.GetById(id);

            if (tag == null)
            {
                return HttpNotFound();
            }

            return View(tag);
        }

        // POST: Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(Guid id)
        {
            try
            {
                var tag = await _tagService.GetById(id);
                if (tag != null)
                {
                    await _tagService.Delete(tag);
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
    }
}
