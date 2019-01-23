using System.Collections.Generic;

namespace Yogging.Domain.Stories
{
    public interface IStoryRepository
    {
        IEnumerable<Story> GetStories();
    }
}