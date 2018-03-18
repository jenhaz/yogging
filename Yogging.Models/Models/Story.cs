using System;
using System.ComponentModel.DataAnnotations;
using Yogging.Models.Enums;

namespace Yogging.Models
{
    public class Story
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Created Date")]
        public string CreatedDate { get; set; }

        [Display(Name = "Last Updated Date")]
        public string LastUpdated { get; set; }

        public Priority Priority { get; set; }

        public TaskType Type { get; set; }

        public string Description { get; set; }

        [Display(Name = "Acceptance Criteria")]
        public string AcceptanceCriteria { get; set; }

        public int Points { get; set; }

        public Status Status { get; set; }

        [Display(Name = "Assigned User")]
        public int? UserId { get; set; }

        [Display(Name = "Sprint")]
        public int? SprintId { get; set; }

        [Display(Name = "Tag")]
        public int? TagId { get; set; }


        public virtual User User { get; set; }
        public virtual Sprint Sprint { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
