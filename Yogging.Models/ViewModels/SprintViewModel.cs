using System;
using System.Collections.Generic;

namespace Yogging.Models.ViewModels
{
    public class SprintViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public IEnumerable<StoryViewModel> Stories { get; set; }
    }
}
