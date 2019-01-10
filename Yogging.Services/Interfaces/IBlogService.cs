using Yogging.Models.ViewModels;

namespace Yogging.Services.Interfaces
{
    public interface IBlogService
    {
        BlogViewModel GetAllBlogPosts();
        BlogViewModel GetAllBlogPosts(string nextPageToken);
    }
}