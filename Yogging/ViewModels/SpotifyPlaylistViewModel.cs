using System.Collections.Generic;

namespace Yogging.ViewModels
{
    public class SpotifyPlaylistViewModel
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string MainImage { get; set; }
        public int TotalTracks { get; set; }
        public IEnumerable<SpotifyTrackViewModel> Tracks { get; set; }
    }
}