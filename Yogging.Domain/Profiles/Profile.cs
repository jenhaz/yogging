﻿using System;

namespace Yogging.Domain.Profiles
{
    public class Profile
    {
        public Guid Id { get; set; }

        public string FullName { get; set; }

        public string ImageUrl { get; set; }

        public string Blurb { get; set; }

        public string LongerBlurb { get; set; }

        public string InstagramUsername { get; set; }

        public string LinkedInUsername { get; set; }

        public string TwitterUsername { get; set; }

        public string BlogUrl { get; set; }

        public string GitHubUsername { get; set; }

        public string CurrentJobTitle { get; set; }

        public string ContactEmailAddress { get; set; }
    }
}
