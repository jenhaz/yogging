using Newtonsoft.Json;
using System.Net;
using System.Web.Configuration;
using Yogging.Models.Models;
using Yogging.Services.Interfaces;

namespace Yogging.Services.Implementations
{
    public class BlogService : IBlogService
    {
        public BlogPosts GetAllBlogPosts()
        {
            string baseUrl = "https://www.googleapis.com/blogger/v3/blogs/";
            string blogId = WebConfigurationManager.AppSettings["BloggerBlogId"].ToString();
            string key = WebConfigurationManager.AppSettings["GoogleApiKey"].ToString();
            string url = baseUrl + blogId + "/posts?key=" + key;

            using (var client = new WebClient())
            {
                var content = client.DownloadString(url);

                var jsonContent = JsonConvert.DeserializeObject<BlogPosts>(content);

                return jsonContent;
            }
        }
    }
}