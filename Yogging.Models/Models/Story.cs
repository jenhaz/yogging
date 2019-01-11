using Yogging.Models.Enums;

namespace Yogging.Models
{
    public class Story
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string CreatedDate { get; set; }
        public string LastUpdated { get; set; }
        public Priority Priority { get; set; }
        public TaskType Type { get; set; }
        public string Description { get; set; }
        public string AcceptanceCriteria { get; set; }
        public int Points { get; set; }
        public StoryStatus Status { get; set; }
        public int? UserId { get; set; }
        public int? SprintId { get; set; }
        public int? TagId { get; set; }
    }
}