using System;
using System.Collections.Generic;
using Yogging.Domain.Stories;
using Yogging.ViewModels;

namespace Yogging.Stories
{
    public interface IStoryService
    {
        IEnumerable<StoryViewModel> GetAllStories();
        IEnumerable<StoryViewModel> GetStoriesBySprint(Guid sprintId);
        IEnumerable<StoryViewModel> GetStoriesByTag(Guid tagId);
        IEnumerable<StoryViewModel> GetStoriesByAssignedUser(Guid userId);
        IEnumerable<StoryViewModel> GetStoriesByStatus(StoryStatus status);
        StoryViewModel GetStory(Story x);
        Story PutStory(StoryViewModel x);
    }
}
