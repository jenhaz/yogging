using System;

namespace Yogging.Domain.Stories
{
    public class Story
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public Priority Priority { get; set; }
        public TaskType Type { get; set; }
        public string Description { get; set; }
        public string AcceptanceCriteria { get; set; }
        public int Points { get; set; }
        public StoryStatus Status { get; set; }
        public Guid? UserId { get; set; }
        public Guid? SprintId { get; set; }
        public Guid? TagId { get; set; }
    }
}