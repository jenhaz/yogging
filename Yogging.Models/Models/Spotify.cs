﻿using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yogging.Models
{
    public class SpotifyPlaylist
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("external_urls")]
        public SpotifyExternalUrl ExternalUrl { get; set; }

        [JsonProperty("public")]
        public bool IsPublic { get; set; }

    }

    public class SpotifyExternalUrl
    {
        [JsonProperty("spotify")]
        public string PlaylistUrl { get; set; }
    }

    public class SpotifyPlaylists
    {
        [JsonProperty("items")]
        public List<SpotifyPlaylist> Playlists { get; set; }
    }

    public class SpotifyToken
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }
}
