using System;
using System.Collections.Generic;
using Yogging.Domain.Stories;

namespace Yogging.Domain.Users
{
    public class User
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public bool IsInactive { get; set; }
        
        public virtual ICollection<Story> Stories { get; set; }
    }
}