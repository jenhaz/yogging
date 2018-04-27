using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;
using Yogging.Models.Models;
using System.Net.Http.Headers;
using System;
using Newtonsoft.Json;
using System.Net;
using System.Web.Script.Serialization;
using System.Linq;
using System.Web.Configuration;

namespace Yogging.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }
        
        public ActionResult Blog()
        {
            BlogPosts posts = GetAllBlogPosts();

            return View(posts);
        }

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