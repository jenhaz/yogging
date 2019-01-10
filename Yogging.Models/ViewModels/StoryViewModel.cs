using System.ComponentModel.DataAnnotations;
using Yogging.Models.Enums;

namespace Yogging.Models.ViewModels
{
    public class StoryViewModel
    {
        public StoryViewModel(
            int id,
            string name,
            string createdDate,
            string lastUpdated,
            Priority priority,
            TaskType type,
            string description,
            string acceptanceCriteria,
            int points,
            StoryStatus status,
            int? userId,
            string userName,
            int? sprintId,
            string sprintName,
            int? tagId,
            string tagColour,
            string tagName)
        {
            Id = id;
            Name = name;
            CreatedDate = createdDate;
            LastUpdated = lastUpdated;
            Priority = priority;
            Type = type;
            Description = description;
            AcceptanceCriteria = acceptanceCriteria;
            Points = points;
            Status = status;
            UserId = userId;
            UserName = userName;
            SprintId = sprintId;
            SprintName = sprintName;
            TagId = tagId;
            TagColour = tagColour;
            TagName = tagName;
        }

        public int Id { get; }

        public string Name { get; }

        [Display(Name = "Created Date")]
        public string CreatedDate { get; }

        [Display(Name = "Last Updated Date")]
        public string LastUpdated { get; }

        public Priority Priority { get; }

        public TaskType Type { get; }

        public string Description { get; }

        [Display(Name = "Acceptance Criteria")]
        public string AcceptanceCriteria { get; }

        public int Points { get; }

        public StoryStatus Status { get; }

        [Display(Name = "User")]
        public int? UserId { get; }

        [Display(Name = "Assigned User")]
        public string UserName { get; }

        [Display(Name = "Sprint Id")]
        public int? SprintId { get; }

        [Display(Name = "Sprint")]
        public string SprintName { get; }

        [Display(Name = "Tag Id")]
        public int? TagId { get; }

        public string TagColour { get; }

        [Display(Name = "Tag Name")]
        public string TagName { get; }
    }
}
