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
        string blogId = WebConfigurationManager.AppSettings["BloggerBlogId"].ToString();
        string key = WebConfigurationManager.AppSettings["GoogleApiKey"].ToString();
        
        private BlogPosts GetBlogPostsJson(string url)
        {
            using (WebClient client = new WebClient())
            {
                string content = client.DownloadString(url);

                BlogPosts jsonContent = JsonConvert.DeserializeObject<BlogPosts>(content);

                return jsonContent;
            }
        }

        /// <summary>
        /// Returns list of Blog Posts using the view model
        /// </summary>
        /// <returns></returns>
        public BlogViewModel GetAllBlogPosts()
        {
            string url = $"https://www.googleapis.com/blogger/v3/blogs/{blogId}/posts?key={key}";
            BlogPosts posts = GetBlogPostsJson(url);

            List<BlogPost> list = posts?.Posts;

            IEnumerable<BlogPostViewModel> vm = list?.Select(x => GetBlogPostViewModel(x)).ToList();

            return new BlogViewModel()
            {
                BlogPosts = vm,
                NextPageToken = posts?.NextPageToken
            };
        }

        public BlogViewModel GetAllBlogPosts(string nextPageToken)
        {
            string url = $"https://www.googleapis.com/blogger/v3/blogs/{blogId}/posts?pageToken={nextPageToken}&key={key}";
            BlogPosts nextPagePosts = GetBlogPostsJson(url);

            List<BlogPost> list = nextPagePosts?.Posts;

            IEnumerable<BlogPostViewModel> vm = list?.Select(x => GetBlogPostViewModel(x)).ToList();

            return new BlogViewModel()
            {
                BlogPosts = vm,
                NextPageToken = nextPagePosts?.NextPageToken
            };
        }

        /// <summary>
        /// Converts model to view model
        /// </summary>
        /// <param name="post">The post retrieved from the API</param>
        /// <returns></returns>
        private BlogPostViewModel GetBlogPostViewModel(BlogPost post)
        {
            string firstImg = GetFirstImageInHtml(post.PostContent);

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

        /// <summary>
        /// Converts the date retrieved from the API to something better looking
        /// </summary>
        /// <param name="date">The date retrieved from the API</param>
        /// <returns></returns>
        private string GetNiceDate(string date)
        {
            string stringDate = Convert.ToDateTime(date).ToString("dd/MM/yyyy");

            return stringDate;
        }

        /// <summary>
        /// Gets the first image from the HTML retrieved from the API
        /// </summary>
        /// <param name="postContent">The string of HTML from the body of the blog post</param>
        /// <returns></returns>
        private string GetFirstImageInHtml(string postContent)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(postContent);
            HtmlNodeCollection nodes = htmlDocument.DocumentNode.SelectNodes("//img");

            if (nodes != null)
            {
                HtmlNode image = nodes.FirstOrDefault();
                HtmlAttribute src = image.Attributes["src"];

                return src.Value;
            }

            return string.Empty;
        }
    }
}