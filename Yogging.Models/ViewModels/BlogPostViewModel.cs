using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yogging.Models.ViewModels
{
    public class BlogPostViewModel
    {
        public string Id { get; set; }
        
        public string PublishedDate { get; set; }
        
        public string UpdatedDate { get; set; }
        
        public string PostUrl { get; set; }
        
        public string PostTitle { get; set; }
        
        public string PostContent { get; set; }

        public string PostMainImage { get; set; }
    }
}
