using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yogging.Domain.Stories;
using Yogging.ViewModels;

namespace Yogging.Stories
{
    public class StoryService : IStoryService
    {
        private readonly IStoryRepository _repository;

        public StoryService(IStoryRepository repository)
        {
            _repository = repository;
        }

        public IEnumerable<StoryViewModel> GetAll()
        {
            var stories = _repository
                .GetAll()
                .Select(GetViewModel);

            return stories;
        }

        public StoryViewModel GetById(Guid id)
        {
            var story = _repository.GetById(id);
            return GetViewModel(story);
        }

        public IEnumerable<StoryViewModel> GetBySprint(Guid sprintId)
        {
            var stories = _repository
                .GetAll()
                .Where(y => y.SprintId.Equals(sprintId))
                .Select(GetViewModel);

            return stories;
        }

        public IEnumerable<StoryViewModel> GetByTag(Guid tagId)
        {
            var stories = _repository
                .GetAll()
                .Where(y => y.TagId.Equals(tagId))
                .Select(GetViewModel);

            return stories;
        }

        public IEnumerable<StoryViewModel> GetByAssignedUser(Guid userId)
        {
            var stories = _repository
                .GetAll()
                .Where(y => y.UserId.Equals(userId))
                .Select(GetViewModel);

            return stories;
        }

        public IEnumerable<StoryViewModel> GetByStatus(StoryStatus status)
        {
            var stories = _repository
                .GetAll()
                .Where(y => y.Status.Equals(status))
                .Select(GetViewModel);

            return stories;
        }

        public void Create(StoryViewModel viewModel)
        {
            var story = PutStory(viewModel);
            _repository.Create(story);
        }

        public async Task Update(StoryViewModel viewModel)
        {
            var story = PutStory(viewModel);
            await _repository.Update(story);
        }

        public void Delete(StoryViewModel viewModel)
        {
            var story = PutStory(viewModel);
            _repository.Delete(story);
        }

        private StoryViewModel GetViewModel(Story x)
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

        private Story PutStory(StoryViewModel x)
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