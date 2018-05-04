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
        private SpotifyTrackViewModel GetTrackVm(SpotifyTrack track)
        {
            return new SpotifyTrackViewModel()
            {
                AlbumName = track.Track.Album.Name,
                AlbumImage = track.Track.Album.AlbumImage.FirstOrDefault().ImageUrl,
                AlbumUrl = track.Track.Album.ExternalUrl.Url,
                ArtistName = track.Track.Artists.FirstOrDefault().Name, //TODO: list them out
                ArtistImage = string.Empty,
                ArtistUrl = track.Track.Artists.FirstOrDefault().ExternalUrl.Url,
                TrackName = track.Track.Name,
                TrackUrl = track.Track.ExternalUrl.Url
            };
        }

        private SpotifyPlaylistViewModel GetPlaylistVm(SpotifyPlaylist playlist)
        {
            return new SpotifyPlaylistViewModel()
            {
                Id = playlist.Id,
                Name = playlist.Name,
                Url = playlist.ExternalUrl.Url,
                MainImage = playlist.PlaylistImage.FirstOrDefault().ImageUrl,
                TotalTracks = playlist.PlaylistTracks.TotalTracks,
                Tracks = playlist.PlaylistTracks.Tracks.Select(x => GetTrackVm(x))
            };
        }

        public IEnumerable<SpotifyPlaylistViewModel> GetAllPlaylists()
        {
            SpotifyPlaylists playlists = GetSpotifyPlaylists();
            IEnumerable<SpotifyPlaylist> list = playlists.Playlists;
            
            foreach(var playlist in list)
            {
                SpotifyPlaylistTracks tracks = GetAllPlaylistTracks(playlist.Id);
                playlist.PlaylistTracks.Tracks = tracks.Tracks.OrderByDescending(x => x.Added).ToList();
            }

            IEnumerable<SpotifyPlaylistViewModel> vmList = list.Select(x => GetPlaylistVm(x));

            return vmList;
        }

        private SpotifyPlaylists GetSpotifyPlaylists()
        {
            string accountId = WebConfigurationManager.AppSettings["SpotifyAccountId"].ToString();
            string url = string.Format("https://api.spotify.com/v1/users/{0}/playlists", accountId);
            SpotifyToken token = GetAccessToken();
            string accessToken = token.access_token;

            SpotifyPlaylists playlists = GetSpotifyPlaylistsJson(accessToken, url);

            return playlists;
        }

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

        private SpotifyPlaylistTracks GetAllPlaylistTracks(string playlistId)
        {
            SpotifyPlaylistTracks tracks = GetSpotifyPlaylistTracks(playlistId);

            return tracks;
        }

        private SpotifyPlaylistTracks GetSpotifyPlaylistTracks(string playlistId)
        {
            string accountId = WebConfigurationManager.AppSettings["SpotifyAccountId"].ToString();
            string url = string.Format("https://api.spotify.com/v1/users/{0}/playlists/{1}/tracks", accountId, playlistId);
            SpotifyToken token = GetAccessToken();
            string accessToken = token.access_token;

            SpotifyPlaylistTracks tracks = GetSpotifyPlaylistTracksJson(accessToken, url);

            return tracks;
        }

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

        private SpotifyToken GetAccessToken()
        {
            SpotifyToken token = new SpotifyToken();
            string postString = string.Format("grant_type=client_credentials");

            byte[] byteArray = Encoding.UTF8.GetBytes(postString);
            string url = "https://accounts.spotify.com/api/token";

            string clientId = WebConfigurationManager.AppSettings["SpotifyClientId"].ToString();
            string secret = WebConfigurationManager.AppSettings["SpotifySecret"].ToString();
            string credentials = string.Format("{0}:{1}", clientId, secret);
            string auth = "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials));

            WebRequest request = WebRequest.Create(url);
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
    }
}