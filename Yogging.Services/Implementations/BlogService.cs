using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Configuration;
using Yogging.Models.Models;
using Yogging.Models.ViewModels;
using Yogging.Services.Interfaces;

namespace Yogging.Services.Implementations
{
    public class BlogService : IBlogService
    {
        private readonly string _blogId = WebConfigurationManager.AppSettings["BloggerBlogId"];
        private readonly string _key = WebConfigurationManager.AppSettings["GoogleApiKey"];

        private BlogPosts GetBlogPostsJson(string url)
        {
            using (var client = new WebClient())
            {
                var content = client.DownloadString(url);

                var jsonContent = JsonConvert.DeserializeObject<BlogPosts>(content);

                return jsonContent;
            }
        }

        public BlogViewModel GetAllBlogPosts()
        {
            var url = $"https://www.googleapis.com/blogger/v3/blogs/{_blogId}/posts?key={_key}";
            var posts = GetBlogPostsJson(url);

            var list = posts?.Posts;

            IEnumerable<BlogPostViewModel> vm = list?.Select(GetBlogPostViewModel).ToList();

            return new BlogViewModel
            {
                BlogPosts = vm,
                NextPageToken = posts?.NextPageToken
            };
        }

        public BlogViewModel GetAllBlogPosts(string nextPageToken)
        {
            var url = $"https://www.googleapis.com/blogger/v3/blogs/{_blogId}/posts?pageToken={nextPageToken}&key={_key}";
            var nextPagePosts = GetBlogPostsJson(url);

            var list = nextPagePosts?.Posts;

            IEnumerable<BlogPostViewModel> vm = list?.Select(GetBlogPostViewModel).ToList();

            return new BlogViewModel
            {
                BlogPosts = vm,
                NextPageToken = nextPagePosts?.NextPageToken
            };
        }

        private BlogPostViewModel GetBlogPostViewModel(BlogPost post)
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

        private string GetNiceDate(string date)
        {
            var stringDate = Convert.ToDateTime(date).ToString("dd/MM/yyyy");

            return stringDate;
        }

        private string GetFirstImageInHtml(string postContent)
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