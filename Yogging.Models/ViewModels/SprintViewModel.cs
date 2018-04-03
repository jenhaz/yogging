using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Yogging.Models.Enums;

namespace Yogging.Models.ViewModels
{
    public class SprintViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        public SprintStatus Status { get; set; }

        [Display(Name = "Total Points")]
        public int SprintPointTotal { get; set; }

        public IEnumerable<StoryViewModel> Stories { get; set; }
    }
}
