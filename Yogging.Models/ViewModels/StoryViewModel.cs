using System.ComponentModel.DataAnnotations;
using Yogging.Models.Enums;

namespace Yogging.Models.ViewModels
{
    public class StoryViewModel
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

        public StoryStatus Status { get; set; }

        [Display(Name = "User")]
        public int? UserId { get; set; }

        [Display(Name = "Assigned User")]
        public string UserName { get; set; }

        [Display(Name = "Sprint Id")]
        public int? SprintId { get; set; }

        [Display(Name = "Sprint")]
        public string SprintName { get; set; }

        [Display(Name = "Tag Id")]
        public int? TagId { get; set; }

        [Display(Name = "Tag Name")]
        public string TagName { get; set; }
    }
}
