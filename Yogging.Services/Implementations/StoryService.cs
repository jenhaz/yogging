using System.Collections.Generic;
using System.Linq;
using Yogging.DAL.Context;
using Yogging.DAL.Repository;
using Yogging.Models;
using Yogging.Models.Enums;
using Yogging.Models.ViewModels;
using Yogging.Services.Interfaces;

namespace Yogging.Services.Implementations
{
    public class StoryService : IStoryService
    {
        private YoggingContext db = new YoggingContext();
        private IContentRepository ContentRepository { get; }

        public StoryService(IContentRepository contentRepository)
        {
            ContentRepository = contentRepository;
        }

        public IEnumerable<StoryViewModel> GetAllStories()
        {
            IEnumerable<StoryViewModel> stories = ContentRepository.GetStories()
                .Select(x => GetStory(x));

            return stories;
        }
        
        public IEnumerable<StoryViewModel> GetStoriesBySprint(int sprintId)
        {
            IEnumerable<StoryViewModel> stories = ContentRepository.GetStories().Where(y => y.SprintId.Equals(sprintId))
                .Select(x => GetStory(x));

            return stories;
        }

        public IEnumerable<StoryViewModel> GetStoriesByTag(int tagId)
        {
            IEnumerable<StoryViewModel> stories = ContentRepository.GetStories().Where(y => y.TagId.Equals(tagId))
                .Select(x => GetStory(x));

            return stories;
        }

        public IEnumerable<StoryViewModel> GetStoriesByAssignedUser(int userId)
        {
            IEnumerable<StoryViewModel> stories = ContentRepository.GetStories().Where(y => y.UserId.Equals(userId))
                .Select(x => GetStory(x));

            return stories;
        }

        public IEnumerable<StoryViewModel> GetStoriesByStatus(StoryStatus status)
        {
            IEnumerable<StoryViewModel> stories = ContentRepository.GetStories().Where(y => y.Status.Equals(status))
                .Select(x => GetStory(x));

            return stories;
        }

        private StoryViewModel GetStory(Story x)
        {
            return new StoryViewModel()
            {
                Id = x.Id,
                Name = !string.IsNullOrEmpty(x.Name) ? x.Name : "Sprint " + x.Id.ToString(),
                CreatedDate = !string.IsNullOrEmpty(x.CreatedDate) ? x.CreatedDate : string.Empty,
                LastUpdated = !string.IsNullOrEmpty(x.LastUpdated) ? x.LastUpdated : string.Empty,
                Priority = x.Priority,
                Type = x.Type,
                Description = !string.IsNullOrEmpty(x.Description) ? x.Description : string.Empty,
                AcceptanceCriteria = !string.IsNullOrEmpty(x.AcceptanceCriteria) ? x.AcceptanceCriteria : string.Empty,
                Points = x.Points,
                Status = x.Status,
                UserId = x.User.Id,
                SprintId = x.Sprint.Id,
                TagId = x.Tag.Id,
                UserName = x.User.FirstName + " " + x.User.LastName,
                SprintName = x.Sprint.Name,
                TagName = x.Tag.Name
            };
        }
    }
}
