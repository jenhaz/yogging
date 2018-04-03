using System.Collections.Generic;

namespace Yogging.Models
{
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public bool IsInactive { get; set; }
        
        public virtual ICollection<Story> Stories { get; set; }
    }
}