using System.Collections.Generic;

namespace Yogging.Models.ViewModels
{
    public class BlogViewModel
    {
        public IEnumerable<BlogPostViewModel> BlogPosts { get; set; }

        public string NextPageToken { get; set; }

        public string PreviousPageToken { get; set; }
    }
}