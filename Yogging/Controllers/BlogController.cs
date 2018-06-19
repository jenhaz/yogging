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
            BlogViewModel posts = BlogService.GetAllBlogPosts();

            return View(posts);
        }

        public ActionResult MoreBlogPosts(string nextPageToken = "")
        {
            if(!string.IsNullOrEmpty(nextPageToken))
            {
                BlogViewModel morePosts = BlogService.GetAllBlogPosts(nextPageToken);
                return PartialView("_BlogPostsList", morePosts);
            }
            else
            {
                return null;
            }
        }
    }
}