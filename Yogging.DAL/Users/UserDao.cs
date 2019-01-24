using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using Yogging.Domain.Stories;

namespace Yogging.DAL.Users
{
    [Table("Users")]
    public class UserDao
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public bool IsInactive { get; set; }

        public virtual ICollection<Story> Stories { get; set; }
    }
}