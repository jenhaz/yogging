using System.Collections.Generic;
using Yogging.Models.Enums;
using Yogging.Models.ViewModels;

namespace Yogging.Services.Interfaces
{
    public interface IStoryService
    {
        IEnumerable<StoryViewModel> GetAllStories();
        IEnumerable<StoryViewModel> GetStoriesBySprint(int sprintId);
        IEnumerable<StoryViewModel> GetStoriesByTag(int tagId);
        IEnumerable<StoryViewModel> GetStoriesByAssignedUser(int userId);
        IEnumerable<StoryViewModel> GetStoriesByStatus(Status status);
    }
}
