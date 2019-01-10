using System.Collections.Generic;
using Yogging.Models;

namespace Yogging.DAL.Repository
{
    public interface IStoryRepository
    {
        IEnumerable<Story> GetStories();
    }
}