using System;
using System.ComponentModel.DataAnnotations;
using Yogging.Domain.Stories;

namespace Yogging.ViewModels
{
    public class StoryViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; set; }

        [Display(Name = "Last Updated Date")]
        public DateTime LastUpdated { get; set; }

        public Priority Priority { get; set; }

        public TaskType Type { get; set; }

        public string Description { get; set; }

        [Display(Name = "Acceptance Criteria")]
        public string AcceptanceCriteria { get; set; }

        public int Points { get; set; }

        public StoryStatus Status { get; set; }

        [Display(Name = "User")]
        public Guid? UserId { get; set; }

        [Display(Name = "Assigned User")]
        public string UserName { get; set; }

        [Display(Name = "Sprint Id")]
        public Guid? SprintId { get; set; }

        [Display(Name = "Sprint")]
        public string SprintName { get; set; }

        [Display(Name = "Tag Id")]
        public Guid? TagId { get; set; }

        public string TagColour { get; set; }

        [Display(Name = "Tag Name")]
        public string TagName { get; set; }
    }
}
