using System.Collections.Generic;
using Yogging.ViewModels;

namespace Yogging.Spotify
{
    public interface ISpotifyService
    {
        IEnumerable<SpotifyPlaylistViewModel> GetAllPlaylists();
    }
}
