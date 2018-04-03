using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Yogging.Models
{
    public class User
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        public string EmailAddress { get; set; }

        [Display(Name = "User is Inactive")]
        public bool IsInactive { get; set; }
        
        public virtual ICollection<Story> Stories { get; set; }
    }
}
