using System;
using System.Web.Mvc;
using Yogging.Helpers;
using Yogging.Tags;
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
        public ActionResult Index()
        {
            var tags = _tagService.GetAll();

            return View(tags);
        }

        // GET: Tags/Details/5
        public ActionResult Details(Guid id)
        {
            var tag = _tagService.GetById(id);

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
        public ActionResult Create(TagViewModel tag)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _tagService.Create(tag);
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
        public ActionResult Edit(Guid id)
        {
            var tag = _tagService.GetById(id);

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
        public ActionResult Edit(TagViewModel tag)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _tagService.Update(tag);
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
        public ActionResult Delete(Guid id)
        {
            var tag = _tagService.GetById(id);

            if (tag == null)
            {
                return HttpNotFound();
            }

            return View(tag);
        }

        // POST: Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            try
            {
                var tag = _tagService.GetById(id);
                if (tag != null)
                {
                    _tagService.Delete(tag);
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
