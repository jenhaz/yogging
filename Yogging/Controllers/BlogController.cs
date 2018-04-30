using System.Collections.Generic;
using System.Web.Mvc;
using Yogging.Models.ViewModels;
using Yogging.Services.Interfaces;

namespace Yogging.Controllers
{
    public class BlogController : Controller
    {
        private IBlogService BlogService { get; }

        public BlogController(IBlogService blogService)
        {
            BlogService = blogService;
        }

        public ActionResult Index()
        {
            IEnumerable<BlogPostViewModel> posts = BlogService.GetAllBlogPosts();

            return View(posts);
        }
    }
}