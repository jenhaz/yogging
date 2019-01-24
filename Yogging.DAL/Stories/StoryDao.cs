using System;
using System.ComponentModel.DataAnnotations.Schema;
using Yogging.DAL.Sprints;
using Yogging.DAL.Tags;
using Yogging.DAL.Users;

namespace Yogging.DAL.Stories
{
    [Table("Stories")]
    public class StoryDao
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public string Priority { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }
        public string AcceptanceCriteria { get; set; }
        public int Points { get; set; }
        public string Status { get; set; }

        public virtual UserDao User { get; set; }
        public Guid? UserId { get; set; }

        public virtual SprintDao Sprint { get; set; }
        public Guid? SprintId { get; set; }

        public virtual TagDao Tag { get; set; }
        public Guid? TagId { get; set; }
    }
}