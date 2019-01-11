using System;
using System.ComponentModel.DataAnnotations;
using Yogging.Models.Enums;

namespace Yogging.Models.ViewModels
{
    public class StoryViewModel
    {
        public StoryViewModel(
            Guid id,
            string name,
            DateTime createdDate,
            DateTime lastUpdated,
            Priority priority,
            TaskType type,
            string description,
            string acceptanceCriteria,
            int points,
            StoryStatus status,
            Guid? userId,
            string userName,
            Guid? sprintId,
            string sprintName,
            Guid? tagId,
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

        public Guid Id { get; }

        public string Name { get; }

        [Display(Name = "Created Date")]
        public DateTime CreatedDate { get; }

        [Display(Name = "Last Updated Date")]
        public DateTime LastUpdated { get; }

        public Priority Priority { get; }

        public TaskType Type { get; }

        public string Description { get; }

        [Display(Name = "Acceptance Criteria")]
        public string AcceptanceCriteria { get; }

        public int Points { get; }

        public StoryStatus Status { get; }

        [Display(Name = "User")]
        public Guid? UserId { get; }

        [Display(Name = "Assigned User")]
        public string UserName { get; }

        [Display(Name = "Sprint Id")]
        public Guid? SprintId { get; }

        [Display(Name = "Sprint")]
        public string SprintName { get; }

        [Display(Name = "Tag Id")]
        public Guid? TagId { get; }

        public string TagColour { get; }

        [Display(Name = "Tag Name")]
        public string TagName { get; }
    }
}
