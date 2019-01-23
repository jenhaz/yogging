using System;
using System.Collections.Generic;

namespace Yogging.ViewModels
{
    public class TagViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public IEnumerable<StoryViewModel> Stories { get; set; }
        public string Colour { get; set; }
    }
}
