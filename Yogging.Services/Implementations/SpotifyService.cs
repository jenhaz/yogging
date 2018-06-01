using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Configuration;
using Yogging.Models;
using Yogging.Models.ViewModels;
using Yogging.Services.Interfaces;

namespace Yogging.Services.Implementations
{
    public class SpotifyService : ISpotifyService
    {
        /// <summary>
        /// Converts the list of playlists retrieved from API to a list of view models & does the same for the tracks within each playlist
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SpotifyPlaylistViewModel> GetAllPlaylists()
        {
            SpotifyPlaylists playlists = GetSpotifyPlaylists();
            IEnumerable<SpotifyPlaylist> list = playlists.Playlists;

            foreach (var playlist in list)
            {
                SpotifyPlaylistTracks tracks = GetAllPlaylistTracks(playlist.Id);
                playlist.PlaylistTracks.Tracks = tracks.Tracks.OrderByDescending(x => x.Added).ToList(); //TODO: somehow get last page first
            }

            IEnumerable<SpotifyPlaylistViewModel> vmList = list.Select(x => GetPlaylistVm(x));

            return vmList;
        }

        /// <summary>
        /// Gets playlists from Spotify API using access token and account ID
        /// </summary>
        /// <returns></returns>
        private SpotifyPlaylists GetSpotifyPlaylists()
        {
            string accountId = WebConfigurationManager.AppSettings["SpotifyAccountId"].ToString();
            string url = $"https://api.spotify.com/v1/users/{accountId}/playlists";
            SpotifyToken token = GetAccessToken();
            string accessToken = token.access_token;

            SpotifyPlaylists playlists = GetSpotifyPlaylistsJson(accessToken, url);

            return playlists;
        }

        /// <summary>
        /// Makes the API call to get all playlists
        /// </summary>
        /// <param name="token">The access token created</param>
        /// <param name="url">The API call url including the account ID parameter</param>
        /// <returns></returns>
        private SpotifyPlaylists GetSpotifyPlaylistsJson(string token, string url)
        {
            WebRequest request = WebRequest.Create(url);
            request.Method = "GET";
            request.Headers.Add("Authorization", "Bearer " + token);
            request.ContentType = "application/json";

            using (WebResponse response = request.GetResponse())
            {
                using (Stream dataStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(dataStream))
                    {
                        string responseFromServer = reader.ReadToEnd();
                        SpotifyPlaylists playlists = JsonConvert.DeserializeObject<SpotifyPlaylists>(responseFromServer);

                        return playlists;
                    }
                }
            }
        }

        /// <summary>
        /// Get the tracks for each playlist
        /// </summary>
        /// <param name="playlistId">ID for the particular Spotify playlist</param>
        /// <returns></returns>
        private SpotifyPlaylistTracks GetAllPlaylistTracks(string playlistId)
        {
            SpotifyPlaylistTracks tracks = GetSpotifyPlaylistTracks(playlistId);

            return tracks;
        }

        /// <summary>
        /// Gets all tracks for the particular playlist
        /// </summary>
        /// <param name="playlistId">ID for the particular Spotify playlist</param>
        /// <returns></returns>
        private SpotifyPlaylistTracks GetSpotifyPlaylistTracks(string playlistId)
        {
            string accountId = WebConfigurationManager.AppSettings["SpotifyAccountId"].ToString();
            string url = $"https://api.spotify.com/v1/users/{accountId}/playlists/{playlistId}/tracks";
            SpotifyToken token = GetAccessToken();
            string accessToken = token.access_token;

            SpotifyPlaylistTracks tracks = GetSpotifyPlaylistTracksJson(accessToken, url);

            return tracks;
        }

        /// <summary>
        /// Makes the API call to get all of the playlist's tracks
        /// </summary>
        /// <param name="token">The access token created</param>
        /// <param name="url">The API url including the account and playlist IDs</param>
        /// <returns></returns>
        private SpotifyPlaylistTracks GetSpotifyPlaylistTracksJson(string token, string url)
        {
            WebRequest request = WebRequest.Create(url);
            request.Method = "GET";
            request.Headers.Add("Authorization", "Bearer " + token);
            request.ContentType = "application/json";

            using (WebResponse response = request.GetResponse())
            {
                using (Stream dataStream = response.GetResponseStream())
                {
                    using (StreamReader reader = new StreamReader(dataStream))
                    {
                        string responseFromServer = reader.ReadToEnd();
                        SpotifyPlaylistTracks tracks = JsonConvert.DeserializeObject<SpotifyPlaylistTracks>(responseFromServer);
                        return tracks;
                    }
                }
            }
        }

        /// <summary>
        /// Creates an access token for API calls
        /// </summary>
        /// <returns></returns>
        private SpotifyToken GetAccessToken()
        {
            SpotifyToken token = new SpotifyToken();
            byte[] byteArray = Encoding.UTF8.GetBytes("grant_type=client_credentials");
            string clientId = WebConfigurationManager.AppSettings["SpotifyClientId"].ToString();
            string secret = WebConfigurationManager.AppSettings["SpotifySecret"].ToString();
            string credentials = $"{clientId}:{secret}";
            string auth = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));

            WebRequest request = WebRequest.Create("https://accounts.spotify.com/api/token");
            request.Method = "POST";
            request.Headers.Add("Authorization", auth);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = byteArray.Length;

            using (Stream dataStream = request.GetRequestStream())
            {
                dataStream.Write(byteArray, 0, byteArray.Length);
                using (WebResponse response = request.GetResponse())
                {
                    using (Stream responseStream = response.GetResponseStream())
                    {
                        using (StreamReader reader = new StreamReader(responseStream))
                        {
                            string responseFromServer = reader.ReadToEnd();
                            token = JsonConvert.DeserializeObject<SpotifyToken>(responseFromServer);
                        }
                    }
                }
            }

            return token;
        }

        /// <summary>
        /// Converts info retrieved from API call to viewmodel
        /// </summary>
        /// <param name="playlist">Playlist info retrieved from API</param>
        /// <returns></returns>
        private SpotifyPlaylistViewModel GetPlaylistVm(SpotifyPlaylist playlist)
        {
            return new SpotifyPlaylistViewModel()
            {
                Id = playlist?.Id,
                Name = playlist?.Name,
                Url = playlist?.ExternalUrl?.Url,
                MainImage = playlist?.PlaylistImage?.FirstOrDefault()?.ImageUrl,
                TotalTracks = playlist.PlaylistTracks.TotalTracks,
                Tracks = playlist?.PlaylistTracks?.Tracks?.Select(x => GetTrackVm(x))
            };
        }

        /// <summary>
        /// Converts info retrieved from API call to viewmodel
        /// </summary>
        /// <param name="track">Track info retrieved from API</param>
        /// <returns></returns>
        private SpotifyTrackViewModel GetTrackVm(SpotifyTrack track)
        {
            return new SpotifyTrackViewModel()
            {
                AlbumName = track?.Track?.Album?.Name,
                AlbumImage = track?.Track?.Album?.AlbumImage?.FirstOrDefault()?.ImageUrl,
                AlbumUrl = track?.Track?.Album?.ExternalUrl?.Url,
                ArtistName = track?.Track?.Artists?.FirstOrDefault()?.Name, //TODO: list them out
                ArtistImage = string.Empty, //TODO: get image 
                ArtistUrl = track?.Track?.Artists?.FirstOrDefault()?.ExternalUrl?.Url,
                TrackName = track?.Track?.Name,
                TrackUrl = track?.Track?.ExternalUrl?.Url
            };
        }
    }
}