using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Yogging.Models.Enums;

namespace Yogging.Models.ViewModels
{
    public class SprintViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Start Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EndDate { get; set; }

        public SprintStatus Status { get; set; }

        [NotMapped]
        [Display(Name = "Total Points")]
        public int SprintPointTotal { get; set; }

        [NotMapped]
        [Display(Name = "Total Points To Do")]
        public int TotalPointsToDo { get; set; }

        [NotMapped]
        [Display(Name = "Total Points In Progress")]
        public int TotalPointsInProgress { get; set; }

        [NotMapped]
        [Display(Name = "Total Points Done")]
        public int TotalPointsDone { get; set; }

        public IEnumerable<StoryViewModel> Stories { get; set; }
    }
}
