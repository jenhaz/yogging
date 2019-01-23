﻿using System;
using System.Collections.Generic;
using Yogging.Domain.Sprints;
using Yogging.Domain.Stories;

namespace Yogging.DAL.Sprints
{
    public class SprintDao
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        
        public DateTime StartDate { get; set; }
        
        public DateTime EndDate { get; set; }

        public SprintStatus Status { get; set; }

        public virtual ICollection<Story> Stories { get; set; }
    }
}