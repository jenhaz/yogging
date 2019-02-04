using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Configuration;
using HtmlAgilityPack;
using Newtonsoft.Json;
using Yogging.ViewModels;

namespace Yogging.Blogs
{
    public class BlogService : IBlogService
    {
        private readonly string _blogId = WebConfigurationManager.AppSettings["BloggerBlogId"];
        private readonly string _key = WebConfigurationManager.AppSettings["GoogleApiKey"];

        public async Task<BlogViewModel> GetAll()
        {
            var url = $"https://www.googleapis.com/blogger/v3/blogs/{_blogId}/posts?key={_key}";
            var posts = await GetJson(url);

            var list = posts?.Posts;

            IEnumerable<BlogPostViewModel> vm = list?.Select(GetViewModel).ToList();

            return new BlogViewModel
            {
                BlogPosts = vm,
                NextPageToken = posts?.NextPageToken
            };
        }

        public async Task<BlogViewModel> GetAll(string nextPageToken)
        {
            var url = $"https://www.googleapis.com/blogger/v3/blogs/{_blogId}/posts?pageToken={nextPageToken}&key={_key}";
            var nextPagePosts = await GetJson(url);

            var list = nextPagePosts?.Posts;

            IEnumerable<BlogPostViewModel> vm = list?.Select(GetViewModel).ToList();

            return new BlogViewModel
            {
                BlogPosts = vm,
                NextPageToken = nextPagePosts?.NextPageToken
            };
        }

        private static async Task<BlogPosts> GetJson(string url)
        {
            using (var client = new WebClient())
            {
                var content = await client.DownloadStringTaskAsync(url);

                var jsonContent = JsonConvert.DeserializeObject<BlogPosts>(content);

                return jsonContent;
            }
        }

        private static BlogPostViewModel GetViewModel(BlogPost post)
        {
            var firstImg = GetFirstImageInHtml(post.PostContent);

            return new BlogPostViewModel
            {
                Id = post.Id,
                PublishedDate = GetNiceDate(post.Published),
                UpdatedDate = post.Updated,
                PostUrl = post.PostUrl,
                PostTitle = post.PostTitle.Replace("&amp;", "&"),
                PostContent = post.PostContent,
                PostMainImage = !string.IsNullOrEmpty(firstImg) ? firstImg : string.Empty
            };
        }

        private static string GetNiceDate(string date)
        {
            var stringDate = Convert.ToDateTime(date).ToString("dd/MM/yyyy");

            return stringDate;
        }

        private static string GetFirstImageInHtml(string postContent)
        {
            var htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(postContent);
            var nodes = htmlDocument.DocumentNode.SelectNodes("//img");

            if (nodes == null)
            {
                return string.Empty;
            }

            var image = nodes.FirstOrDefault();
            var src = image?.Attributes["src"];

            return src?.Value;
        }
    }
}