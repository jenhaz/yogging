using System;
using System.ComponentModel.DataAnnotations;

namespace Yogging.ViewModels
{
    public class ProfileViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "Full name")]
        public string FullName { get; set; }
        
        [Display(Name = "Image url")]
        public string ImageUrl { get; set; }

        public string Blurb { get; set; }

        [Display(Name = "Longer blurb")]
        public string LongerBlurb { get; set; }

        [Display(Name = "Instagram username")]
        public string InstagramUsername { get; set; }

        public string InstagramUrl { get; set; }

        [Display(Name = "Linked In username")]
        public string LinkedInUsername { get; set; }

        public string LinkedInUrl { get; set; }

        [Display(Name = "Twitter Username")]
        public string TwitterUsername { get; set; }

        public string TwitterUrl { get; set; }

        [Display(Name = "Github username")]
        public string GitHubUsername { get; set; }

        public string GitHubUrl { get; set; }

        [Display(Name = "Blog url")]
        public string BlogUrl { get; set; }

        [Display(Name = "Current job title")]
        public string CurrentJobTitle { get; set; }

        [Display(Name = "Contact email address")]
        public string ContactEmailAddress { get; set; }
    }
}