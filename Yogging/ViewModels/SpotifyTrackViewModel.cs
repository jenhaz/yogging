﻿using System.Collections.Generic;

namespace Yogging.ViewModels
{
    public class SpotifyTrackViewModel
    {
        public string AlbumName { get; set; }
        public string AlbumImage { get; set; }
        public string AlbumUrl { get; set; }

        public IEnumerable<SpotifyArtistViewModel> Artists { get; set; }

        public string TrackName { get; set; }
        public string TrackUrl { get; set; }
    }

    public class SpotifyArtistViewModel
    {
        public string ArtistName { get; set; }
        public string ArtistImage { get; set; }
        public string ArtistUrl { get; set; }
    }
}