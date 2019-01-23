using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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

        public BlogViewModel GetAll()
        {
            var url = $"https://www.googleapis.com/blogger/v3/blogs/{_blogId}/posts?key={_key}";
            var posts = GetJson(url);

            var list = posts?.Posts;

            IEnumerable<BlogPostViewModel> vm = list?.Select(GetViewModel).ToList();

            return new BlogViewModel
            {
                BlogPosts = vm,
                NextPageToken = posts?.NextPageToken
            };
        }

        public BlogViewModel GetAll(string nextPageToken)
        {
            var url = $"https://www.googleapis.com/blogger/v3/blogs/{_blogId}/posts?pageToken={nextPageToken}&key={_key}";
            var nextPagePosts = GetJson(url);

            var list = nextPagePosts?.Posts;

            IEnumerable<BlogPostViewModel> vm = list?.Select(GetViewModel).ToList();

            return new BlogViewModel
            {
                BlogPosts = vm,
                NextPageToken = nextPagePosts?.NextPageToken
            };
        }

        private static BlogPosts GetJson(string url)
        {
            using (var client = new WebClient())
            {
                var content = client.DownloadString(url);

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