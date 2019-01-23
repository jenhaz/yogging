using System.ComponentModel.DataAnnotations;

namespace Yogging.Domain.Stories
{
    public enum StoryStatus
    {
        None,
        [Display(Name = "To Do")]
        ToDo,
        [Display(Name = "In Progress")]
        InProgress,
        Done
    }
}
