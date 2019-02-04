using System.Threading.Tasks;
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

        public async Task<ActionResult> Index()
        {
            var posts = await _blogService.GetAll();

            return View(posts);
        }

        public async Task<ActionResult> MoreBlogPosts(string nextPageToken = "")
        {
            if (string.IsNullOrEmpty(nextPageToken))
            {
                return null;
            }

            var morePosts = await _blogService.GetAll(nextPageToken);
            return PartialView("_BlogPostsList", morePosts);
        }
    }
}