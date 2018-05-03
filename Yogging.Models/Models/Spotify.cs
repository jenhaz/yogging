using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yogging.Models
{
    public class SpotifyPlaylists
    {
        [JsonProperty("items")]
        public List<SpotifyPlaylist> Playlists { get; set; }
    }

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

        [JsonProperty("images")]
        public SpotifyImages[] PlaylistImage { get; set; }

        [JsonProperty("tracks")]
        public SpotifyPlaylistTrackInfo PlaylistTracks { get; set; }
    }

    public class SpotifyExternalUrl
    {
        [JsonProperty("spotify")]
        public string Url { get; set; }
    }

    public class SpotifyImages
    {
        [JsonProperty("url")]
        public string ImageUrl { get; set; }
    }
    
    public class SpotifyPlaylistTrackInfo
    {
        [JsonProperty("total")]
        public int TotalTracks { get; set; }

        [JsonProperty("href")]
        public string TracksUrl { get; set; }
        
        public List<SpotifyTrack> Tracks { get; set; }
    }

    public class SpotifyPlaylistTracks
    {
        [JsonProperty("items")]
        public List<SpotifyTrack> Tracks { get; set; }
    }

    public class SpotifyTrack
    {
        [JsonProperty("track")]
        public SpotifyTrackInfo Track { get; set; }
    }

    public class SpotifyTrackInfo
    {
        [JsonProperty("album")]
        public SpotifyAlbum Album { get; set; }

        [JsonProperty("artists")]
        public SpotifyArtist[] Artists { get; set; }

        [JsonProperty("external_urls")]
        public SpotifyExternalUrl ExternalUrl { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class SpotifyAlbum
    {
        [JsonProperty("external_urls")]
        public SpotifyExternalUrl ExternalUrl { get; set; }

        [JsonProperty("images")]
        public SpotifyImages[] AlbumImage { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

    }

    public class SpotifyArtist
    {
        [JsonProperty("external_urls")]
        public SpotifyExternalUrl ExternalUrl { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }
    }

    public class SpotifyToken
    {
        public string access_token { get; set; }
        public string token_type { get; set; }
        public int expires_in { get; set; }
    }
}
