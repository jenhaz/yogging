using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Yogging.ViewModels
{
    public class UserViewModel
    {
        public Guid Id { get; set; }

        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        
        public string FullName => FirstName + " " + LastName;

        [Display(Name = "Email")]
        public string EmailAddress { get; set; }

        [Display(Name = "User Is Inactive")]
        public string IsInactive { get; set; }

        public IEnumerable<StoryViewModel> Stories { get; set; }
    }
}