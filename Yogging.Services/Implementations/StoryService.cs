using System.Collections.Generic;
using System.Linq;
using Yogging.DAL.Repository;
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

        /// <summary>
        /// Get all stories from Repository and convert to viewmodel
        /// </summary>
        /// <returns></returns>
        public IEnumerable<StoryViewModel> GetAllStories()
        {
            var stories = _repository
                .GetStories()
                .Select(GetStory);

            return stories;
        }

        /// <summary>
        /// Get all stories by sprint ID and convert to viewmodel
        /// </summary>
        /// <param name="sprintId">ID of the particular sprint</param>
        /// <returns></returns>
        public IEnumerable<StoryViewModel> GetStoriesBySprint(int sprintId)
        {
            var stories = _repository
                .GetStories()
                .Where(y => y.SprintId.Equals(sprintId))
                .Select(GetStory);

            return stories;
        }

        /// <summary>
        /// Get all stories by Tag and convert to viewmodel
        /// </summary>
        /// <param name="tagId">ID of the particular tag</param>
        /// <returns></returns>
        public IEnumerable<StoryViewModel> GetStoriesByTag(int tagId)
        {
            var stories = _repository
                .GetStories()
                .Where(y => y.TagId.Equals(tagId))
                .Select(GetStory);

            return stories;
        }

        /// <summary>
        /// Get all stories by the assigned user and convert to viewmodel
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public IEnumerable<StoryViewModel> GetStoriesByAssignedUser(int userId)
        {
            var stories = _repository
                .GetStories()
                .Where(y => y.UserId.Equals(userId))
                .Select(GetStory);

            return stories;
        }

        /// <summary>
        /// Get all stories by their status and convert to viewmodel
        /// </summary>
        /// <param name="status"></param>
        /// <returns></returns>
        public IEnumerable<StoryViewModel> GetStoriesByStatus(StoryStatus status)
        {
            var stories = _repository
                .GetStories()
                .Where(y => y.Status.Equals(status))
                .Select(GetStory);

            return stories;
        }

        /// <summary>
        /// Convert story to viewmodel
        /// </summary>
        /// <param name="x">Story stored in db</param>
        /// <returns></returns>
        public StoryViewModel GetStory(Story x)
        {
            return new StoryViewModel
            (
                x.Id,
                !string.IsNullOrEmpty(x.Name) ? x.Name : "Sprint " + x.Id,
                !string.IsNullOrEmpty(x.CreatedDate) ? x.CreatedDate : string.Empty,
                !string.IsNullOrEmpty(x.LastUpdated) ? x.LastUpdated : string.Empty,
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

        /// <summary>
        /// Convert viewmodel to db model
        /// </summary>
        /// <param name="x">Story viewmodel</param>
        /// <returns></returns>
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