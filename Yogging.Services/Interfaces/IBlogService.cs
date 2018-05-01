using System.Collections.Generic;
using Yogging.Models.ViewModels;

namespace Yogging.Services.Interfaces
{
    public interface IBlogService
    {
        IEnumerable<BlogPostViewModel> GetAllBlogPosts();
    }
}