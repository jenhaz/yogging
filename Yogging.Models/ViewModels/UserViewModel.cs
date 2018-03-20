using System.Collections.Generic;

namespace Yogging.Models.ViewModels
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string EmailAddress { get; set; }
        public bool IsInactive { get; set; }

        public IEnumerable<StoryViewModel> Stories { get; set; }
    }
}
