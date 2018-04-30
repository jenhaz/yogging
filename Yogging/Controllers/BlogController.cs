using System.Web.Mvc;
using Yogging.Models.Models;
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
            BlogPosts posts = BlogService.GetAllBlogPosts();

            return View(posts);
        }
    }
}