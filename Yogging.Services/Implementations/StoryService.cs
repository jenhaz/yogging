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

        public IEnumerable<StoryViewModel> GetStoriesByStatus(Status status)
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
                Name = x.Name,
                CreatedDate = x.CreatedDate,
                LastUpdated = x.LastUpdated,
                Priority = x.Priority,
                Type = x.Type,
                Description = x.Description,
                AcceptanceCriteria = x.AcceptanceCriteria,
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
