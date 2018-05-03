using System.Collections.Generic;
using Yogging.Models;

namespace Yogging.Services.Interfaces
{
    public interface ISpotifyService
    {
        IEnumerable<SpotifyPlaylist> GetAllPlaylists();
    }
}
