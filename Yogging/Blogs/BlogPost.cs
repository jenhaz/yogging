using System.Collections.Generic;
using Newtonsoft.Json;

namespace Yogging.Blogs
{
    public class BlogPost
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("published")]
        public string Published { get; set; }

        [JsonProperty("updated")]
        public string Updated { get; set; }

        [JsonProperty("url")]
        public string PostUrl { get; set; }

        [JsonProperty("title")]
        public string PostTitle { get; set; }

        [JsonProperty("content")]
        public string PostContent { get; set; }
    }

    public class BlogPosts
    {
        [JsonProperty("items")]
        public List<BlogPost> Posts { get; set; }

        [JsonProperty("nextPageToken")]
        public string NextPageToken { get; set; }
    }
}