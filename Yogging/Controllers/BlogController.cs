using System.Web.Mvc;
using Yogging.Blogs;

namespace Yogging.Controllers
{
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        public ActionResult Index()
        {
            var posts = _blogService.GetAll();

            return View(posts);
        }

        public ActionResult MoreBlogPosts(string nextPageToken = "")
        {
            if (string.IsNullOrEmpty(nextPageToken))
            {
                return null;
            }

            var morePosts = _blogService.GetAll(nextPageToken);
            return PartialView("_BlogPostsList", morePosts);
        }
    }
}