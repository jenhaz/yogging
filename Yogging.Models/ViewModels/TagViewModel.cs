using System.Collections.Generic;

namespace Yogging.Models.ViewModels
{
    public class TagViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<StoryViewModel> Stories { get; set; }
    }
}
