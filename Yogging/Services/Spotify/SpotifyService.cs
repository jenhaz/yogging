using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Configuration;
using Newtonsoft.Json;
using Yogging.ViewModels;

namespace Yogging.Services.Spotify
{
    public class SpotifyService : ISpotifyService
    {
        private readonly string _accountId = WebConfigurationManager.AppSettings["SpotifyAccountId"];
        private readonly string _clientId = WebConfigurationManager.AppSettings["SpotifyClientId"];
        private readonly string _secret = WebConfigurationManager.AppSettings["SpotifySecret"];

        public async Task<IEnumerable<SpotifyPlaylistViewModel>> GetAllPlaylists()
        {
            var playlists = await GetSpotifyPlaylists();

            foreach (var playlist in playlists)
            {
                var tracks = await GetSpotifyPlaylistTracks(playlist.Id);
                playlist.PlaylistTracks.Tracks = tracks.OrderByDescending(x => x.Added).ToList(); //TODO: somehow get last page first
            }

            var vmList = playlists.Select(GetPlaylistVm);

            return vmList;
        }

        private async Task<List<SpotifyPlaylist>> GetSpotifyPlaylists()
        {
            var url = $"https://api.spotify.com/v1/users/{_accountId}/playlists";
            var token = await GetAccessToken();
            var playlists = await GetSpotifyPlaylistsJson(token, url);

            return playlists;
        }

        private static async Task<List<SpotifyPlaylist>> GetSpotifyPlaylistsJson(string token, string url)
        {
            var playlists = new SpotifyPlaylists();
            var request = CreateGetRequest(token, url);

            using (var response = request.GetResponse())
            using (var dataStream = response.GetResponseStream())
            {
                if (dataStream != null)
                {
                    using (var reader = new StreamReader(dataStream))
                    {
                        var responseFromServer = await reader.ReadToEndAsync();
                        playlists = JsonConvert.DeserializeObject<SpotifyPlaylists>(responseFromServer);

                        return playlists.Playlists;
                    }
                }
            }

            return playlists.Playlists;
        }

        private async Task<List<SpotifyTrack>> GetSpotifyPlaylistTracks(string playlistId)
        {
            var url = $"https://api.spotify.com/v1/users/{_accountId}/playlists/{playlistId}/tracks";
            var token = await GetAccessToken();
            var tracks = await GetSpotifyPlaylistTracksJson(token, url);

            return tracks.Tracks;
        }

        private static async Task<SpotifyPlaylistTracks> GetSpotifyPlaylistTracksJson(string token, string url)
        {
            var tracks = new SpotifyPlaylistTracks();
            var request = CreateGetRequest(token, url);

            using (var response = request.GetResponse())
            using (var dataStream = response.GetResponseStream())
            {
                if (dataStream != null)
                {
                    using (var reader = new StreamReader(dataStream))
                    {
                        var responseFromServer = await reader.ReadToEndAsync();
                        tracks = JsonConvert.DeserializeObject<SpotifyPlaylistTracks>(responseFromServer);
                        return tracks;
                    }
                }
            }
            
            return tracks;
        }

        private async Task<string> GetAccessToken()
        {
            var token = new SpotifyToken();
            var byteArray = Encoding.UTF8.GetBytes("grant_type=client_credentials");
            var credentials = $"{_clientId}:{_secret}";
            var auth = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));
            var request = CreatePostRequest(auth, byteArray);

            using (var dataStream = await request.GetRequestStreamAsync())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                using (var response = await request.GetResponseAsync())
                using (var responseStream = response.GetResponseStream())
                {
                    if (responseStream != null)
                    {
                        using (var reader = new StreamReader(responseStream))
                        {
                            var responseFromServer = await reader.ReadToEndAsync();
                            token = JsonConvert.DeserializeObject<SpotifyToken>(responseFromServer);
                        }
                    }
                }
            }

            return token.access_token;
        }

        private static WebRequest CreateGetRequest(string token, string url)
        {
            var request = WebRequest.Create(url);
            request.Method = "GET";
            request.Headers.Add("Authorization", "Bearer " + token);
            request.ContentType = "application/json";
            return request;
        }

        private static WebRequest CreatePostRequest(string auth, byte[] byteArray)
        {
            var request = WebRequest.Create("https://accounts.spotify.com/api/token");
            request.Method = "POST";
            request.Headers.Add("Authorization", auth);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;
            return request;
        }

        private static SpotifyPlaylistViewModel GetPlaylistVm(SpotifyPlaylist playlist)
        {
            return new SpotifyPlaylistViewModel
            {
                Id = playlist?.Id,
                Name = playlist?.Name,
                Url = playlist?.ExternalUrl?.Url,
                MainImage = playlist?.PlaylistImage?.FirstOrDefault()?.ImageUrl,
                TotalTracks = playlist?.PlaylistTracks.TotalTracks ?? 0,
                Tracks = playlist?.PlaylistTracks?.Tracks?.Select(GetTrackVm)
            };
        }

        private static SpotifyTrackViewModel GetTrackVm(SpotifyTrack track)
        {
            var artists = GetAllArtists(track?.Track?.Artists);

            return new SpotifyTrackViewModel
            {
                AlbumName = track?.Track?.Album?.Name,
                AlbumImage = track?.Track?.Album?.AlbumImage?.FirstOrDefault()?.ImageUrl,
                AlbumUrl = track?.Track?.Album?.ExternalUrl?.Url,
                Artists = artists,
                TrackName = track?.Track?.Name,
                TrackUrl = track?.Track?.ExternalUrl?.Url
            };
        }

        private static IEnumerable<SpotifyArtistViewModel> GetAllArtists(IEnumerable<SpotifyArtist> artists)
        {
            var allArtists = new List<SpotifyArtistViewModel>();

            foreach (var artist in artists)
            {
                allArtists.Add(new SpotifyArtistViewModel
                {
                    ArtistName = artist.Name,
                    ArtistImage = "",
                    ArtistUrl = artist.ExternalUrl.Url
                });
            }

            return allArtists;
        }
    }
}