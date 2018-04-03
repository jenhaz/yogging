using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Yogging.Models.Enums;

namespace Yogging.Models
{
    public class Sprint
    {
        public int Id { get; set; }

        public string Name { get; set; }

        [Display(Name = "Start Date")]
        public DateTime StartDate { get; set; }

        [Display(Name = "End Date")]
        public DateTime EndDate { get; set; }

        public SprintStatus Status { get; set; }

        public virtual ICollection<Story> Stories { get; set; }
    }
}