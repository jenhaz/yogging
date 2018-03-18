using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yogging.Models.Enums;

namespace Yogging.Models.ViewModels
{
    public class StoryViewModel
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
        public Status Status { get; set; }
        public int? UserId { get; set; }
        public string UserName { get; set; }
        public int? SprintId { get; set; }
        public string SprintName { get; set; }
        public int? TagId { get; set; }
        public string TagName { get; set; }

        //public virtual User User { get; set; }
        //public virtual Sprint Sprint { get; set; }
        //public virtual Tag Tag { get; set; }
    }
}
