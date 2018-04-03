using System.ComponentModel.DataAnnotations;

namespace Yogging.Models.Enums
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
