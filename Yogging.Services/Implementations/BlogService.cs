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
        /// <summary>
        /// Returns a list of Blog Posts from json content retrieved from the API
        /// </summary>
        /// <returns></returns>
        private BlogPosts GetAllBlogPostsJson()
        {
            string blogId = WebConfigurationManager.AppSettings["BloggerBlogId"].ToString();
            string key = WebConfigurationManager.AppSettings["GoogleApiKey"].ToString();
            string url = $"https://www.googleapis.com/blogger/v3/blogs/{blogId}/posts?key={key}";

            using (var client = new WebClient())
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
        public IEnumerable<BlogPostViewModel> GetAllBlogPosts()
        {
            BlogPosts posts = GetAllBlogPostsJson();

            List<BlogPost> list = posts.Posts;

            IEnumerable<BlogPostViewModel> vm = list.Select(x => GetBlogPostViewModel(x)).ToList();

            return vm;
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
                PostTitle = post.PostTitle,
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