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
            var list = playlists.Playlists;

            foreach (var playlist in list)
            {
                var tracks = await GetAllPlaylistTracks(playlist.Id);
                playlist.PlaylistTracks.Tracks = tracks.Tracks.OrderByDescending(x => x.Added).ToList(); //TODO: somehow get last page first
            }

            var vmList = list.Select(GetPlaylistVm);

            return vmList;
        }

        private async Task<SpotifyPlaylists> GetSpotifyPlaylists()
        {
            var url = $"https://api.spotify.com/v1/users/{_accountId}/playlists";
            var token = await GetAccessToken();
            var accessToken = token.access_token;

            var playlists = await GetSpotifyPlaylistsJson(accessToken, url);

            return playlists;
        }

        private async Task<SpotifyPlaylists> GetSpotifyPlaylistsJson(string token, string url)
        {
            var playlists = new SpotifyPlaylists();
            var request = WebRequest.Create(url);
            request.Method = "GET";
            request.Headers.Add("Authorization", "Bearer " + token);
            request.ContentType = "application/json";

            using (var response = request.GetResponse())
            using (var dataStream = response.GetResponseStream())
            {
                if (dataStream != null)
                {
                    using (var reader = new StreamReader(dataStream))
                    {
                        var responseFromServer = await reader.ReadToEndAsync();
                        playlists = JsonConvert.DeserializeObject<SpotifyPlaylists>(responseFromServer);

                        return playlists;
                    }
                }
            }

            return playlists;
        }

        private async Task<SpotifyPlaylistTracks> GetAllPlaylistTracks(string playlistId)
        {
            var tracks = await GetSpotifyPlaylistTracks(playlistId);

            return tracks;
        }

        private async Task<SpotifyPlaylistTracks> GetSpotifyPlaylistTracks(string playlistId)
        {
            var url = $"https://api.spotify.com/v1/users/{_accountId}/playlists/{playlistId}/tracks";
            var token = await GetAccessToken();
            var accessToken = token.access_token;

            var tracks = await GetSpotifyPlaylistTracksJson(accessToken, url);

            return tracks;
        }

        private async Task<SpotifyPlaylistTracks> GetSpotifyPlaylistTracksJson(string token, string url)
        {
            var tracks = new SpotifyPlaylistTracks();
            var request = WebRequest.Create(url);
            request.Method = "GET";
            request.Headers.Add("Authorization", "Bearer " + token);
            request.ContentType = "application/json";

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

        private async Task<SpotifyToken> GetAccessToken()
        {
            var token = new SpotifyToken();
            var byteArray = Encoding.UTF8.GetBytes("grant_type=client_credentials");
            var credentials = $"{_clientId}:{_secret}";
            var auth = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));

            var request = WebRequest.Create("https://accounts.spotify.com/api/token");
            request.Method = "POST";
            request.Headers.Add("Authorization", auth);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

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

            return token;
        }

        private SpotifyPlaylistViewModel GetPlaylistVm(SpotifyPlaylist playlist)
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

        private SpotifyTrackViewModel GetTrackVm(SpotifyTrack track)
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

        private IEnumerable<SpotifyArtistViewModel> GetAllArtists(IEnumerable<SpotifyArtist> artists)
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