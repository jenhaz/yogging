using System;
using System.Collections.Generic;
using System.Linq;
using Yogging.Domain.Stories;
using Yogging.Models;
using Yogging.Models.Enums;
using Yogging.Models.ViewModels;
using Yogging.Services.Interfaces;

namespace Yogging.Services.Implementations
{
    public class StoryService : IStoryService
    {
        private readonly IStoryRepository _repository;

        public StoryService(IStoryRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<StoryViewModel> GetAllStories()
        {
            var stories = _repository
                .GetStories()
                .Select(GetStory);

            return stories;
        }

        public IEnumerable<StoryViewModel> GetStoriesBySprint(Guid sprintId)
        {
            var stories = _repository
                .GetStories()
                .Where(y => y.SprintId.Equals(sprintId))
                .Select(GetStory);

            return stories;
        }

        public IEnumerable<StoryViewModel> GetStoriesByTag(Guid tagId)
        {
            var stories = _repository
                .GetStories()
                .Where(y => y.TagId.Equals(tagId))
                .Select(GetStory);

            return stories;
        }

        public IEnumerable<StoryViewModel> GetStoriesByAssignedUser(Guid userId)
        {
            var stories = _repository
                .GetStories()
                .Where(y => y.UserId.Equals(userId))
                .Select(GetStory);

            return stories;
        }

        public IEnumerable<StoryViewModel> GetStoriesByStatus(StoryStatus status)
        {
            var stories = _repository
                .GetStories()
                .Where(y => y.Status.Equals(status))
                .Select(GetStory);

            return stories;
        }

        public StoryViewModel GetStory(Story x)
        {
            return new StoryViewModel
            (
                x.Id,
                !string.IsNullOrEmpty(x.Name) ? x.Name : "Sprint " + x.Id,
                x.CreatedDate,
                x.LastUpdated,
                x.Priority,
                x.Type,
                !string.IsNullOrEmpty(x.Description) ? x.Description : string.Empty,
                !string.IsNullOrEmpty(x.AcceptanceCriteria) ? x.AcceptanceCriteria : string.Empty,
                x.Points,
                x.Status,
                x.UserId,
                string.Empty,
                //!string.IsNullOrEmpty(x.User?.FirstName) && !string.IsNullOrEmpty(x.User?.LastName)
                //    ? x.User?.FirstName + " " + x.User?.LastName
                //    : string.Empty,
                x.SprintId,
                string.Empty,
                //!string.IsNullOrEmpty(x.Sprint?.Name) ? x.Sprint?.Name : string.Empty,
                x.TagId,
                "#ffffff",
                //!string.IsNullOrEmpty(x.Tag?.Colour) ? x.Tag?.Colour : "#ffffff",
                string.Empty
                //!string.IsNullOrEmpty(x.Tag?.Name) ? x.Tag?.Name : string.Empty
            );
        }

        public Story PutStory(StoryViewModel x)
        {
            return new Story
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
                UserId = x.UserId,
                SprintId = x.SprintId,
                TagId = x.TagId
            };
        }
    }
}