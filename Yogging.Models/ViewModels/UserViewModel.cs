using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Yogging.Models.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Display(Name = "Email")]
        public string EmailAddress { get; set; }

        [Display(Name = "User Is Inactive")]
        public bool IsInactive { get; set; }

        public IEnumerable<StoryViewModel> Stories { get; set; }
    }
}