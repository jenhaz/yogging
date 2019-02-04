using System.Collections.Generic;
using System.Threading.Tasks;
using Yogging.ViewModels;

namespace Yogging.Spotify
{
    public interface ISpotifyService
    {
        Task<IEnumerable<SpotifyPlaylistViewModel>> GetAllPlaylists();
    }
}
