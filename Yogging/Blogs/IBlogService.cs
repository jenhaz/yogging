using Yogging.ViewModels;

namespace Yogging.Blogs
{
    public interface IBlogService
    {
        BlogViewModel GetAll();
        BlogViewModel GetAll(string nextPageToken);
    }
}