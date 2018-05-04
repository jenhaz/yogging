using System.Collections.Generic;
using Yogging.Models.ViewModels;

namespace Yogging.Services.Interfaces
{
    public interface ISpotifyService
    {
        IEnumerable<SpotifyPlaylistViewModel> GetAllPlaylists();
    }
}
