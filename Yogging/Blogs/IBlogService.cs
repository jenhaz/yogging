using System.Threading.Tasks;
using Yogging.ViewModels;

namespace Yogging.Blogs
{
    public interface IBlogService
    {
        Task<BlogViewModel> GetAll(string nextPageToken = "");
    }
}