using Yogging.ViewModels;

namespace Yogging.Blogs
{
    public interface IBlogService
    {
        BlogViewModel GetAllBlogPosts();
        BlogViewModel GetAllBlogPosts(string nextPageToken);
    }
}