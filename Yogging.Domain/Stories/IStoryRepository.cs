using System.Collections.Generic;
using Yogging.Models;

namespace Yogging.Domain.Stories
{
    public interface IStoryRepository
    {
        IEnumerable<Story> GetStories();
    }
}