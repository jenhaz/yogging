using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net;
using System.Web.Configuration;
using Yogging.Models.Models;
using Yogging.Models.ViewModels;
using Yogging.Services.Interfaces;
using System.Linq;

namespace Yogging.Services.Implementations
{
    public class BlogService : IBlogService
    {
        private BlogPosts GetAllBlogPostsJson()
        {
            string baseUrl = "https://www.googleapis.com/blogger/v3/blogs/";
            string blogId = WebConfigurationManager.AppSettings["BloggerBlogId"].ToString();
            string key = WebConfigurationManager.AppSettings["GoogleApiKey"].ToString();
            string url = baseUrl + blogId + "/posts?key=" + key;

            using (var client = new WebClient())
            {
                string content = client.DownloadString(url);

                BlogPosts jsonContent = JsonConvert.DeserializeObject<BlogPosts>(content);

                return jsonContent;
            }
        }

        public IEnumerable<BlogPostViewModel> GetAllBlogPosts()
        {
            BlogPosts posts = GetAllBlogPostsJson();

            List<BlogPost> list = posts.Posts;

            IEnumerable<BlogPostViewModel> vm = list.Select(x => GetBlogPostViewModel(x)).ToList();

            return vm;
        }

        private BlogPostViewModel GetBlogPostViewModel(BlogPost post)
        {
            return new BlogPostViewModel
            {
                Id = post.Id,
                PublishedDate = post.Published,
                UpdatedDate = post.Updated,
                PostUrl = post.PostUrl,
                PostTitle = post.PostTitle,
                PostContent = post.PostContent
            };
        }
    }
}