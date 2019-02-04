using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yogging.Domain.Sprints;
using Yogging.Domain.Stories;
using Yogging.Stories;
using Yogging.ViewModels;

namespace Yogging.Sprints
{
    public class SprintService : ISprintService
    {
        private readonly ISprintRepository _sprintRepository;
        private readonly IStoryService _storyService;

        public SprintService(
            ISprintRepository sprintRepository, 
            IStoryService storyService)
        {
            _sprintRepository = sprintRepository;
            _storyService = storyService;
        }

        public async Task<IEnumerable<SprintViewModel>> GetAll()
        {
            var sprints = await _sprintRepository.GetAll();
            var tasks = sprints.Select(GetViewModel);
            return await Task.WhenAll(tasks);
        }

        public async Task<IEnumerable<SprintViewModel>> GetActive()
        {
            var sprints = await _sprintRepository.GetAll();
            var tasks = sprints
                .Where(y => y.Status != SprintStatus.Closed)
                .Select(GetViewModel);
            return await Task.WhenAll(tasks);
        }

        public async Task<IEnumerable<SprintViewModel>> GetClosed()
        {
            var sprints = await _sprintRepository.GetAll();
            var tasks = sprints
                .Where(y => y.Status == SprintStatus.Closed)
                .Select(GetViewModel);
            return await Task.WhenAll(tasks);
        }

        public async Task<SprintViewModel> GetById(Guid id)
        {
            var sprint = await _sprintRepository.GetById(id);
            return await GetViewModel(sprint);
        }

        public void Create(SprintViewModel viewModel)
        {
            var sprint = GetSprint(viewModel);
            _sprintRepository.Create(sprint);
        }

        public void Update(SprintViewModel viewModel)
        {
            var sprint = GetSprint(viewModel);
            _sprintRepository.Update(sprint);
        }

        public void Delete(SprintViewModel viewModel)
        {
            var sprint = GetSprint(viewModel);
            _sprintRepository.Delete(sprint);
        }

        private async Task<SprintViewModel> GetViewModel(Sprint sprint)
        {
            return new SprintViewModel
            {
                Id = sprint.Id,
                Name = sprint.Name,
                StartDate = sprint.StartDate,
                EndDate = sprint.EndDate,
                Stories = await _storyService.GetBySprint(sprint.Id),
                Status = sprint.Status,
                SprintPointTotal = await GetSprintPointTotal(sprint.Id),
                TotalPointsToDo = await GetSprintPointTotal(sprint.Id, StoryStatus.ToDo),
                TotalPointsInProgress = await GetSprintPointTotal(sprint.Id, StoryStatus.InProgress),
                TotalPointsDone = await GetSprintPointTotal(sprint.Id, StoryStatus.Done)
            };
        }

        private static Sprint GetSprint(SprintViewModel sprint)
        {
            return new Sprint
            {
                Id = sprint.Id,
                Name = sprint.Name,
                StartDate = sprint.StartDate,
                EndDate = sprint.EndDate,
                Status = sprint.Status
            };
        }

        private async Task<int> GetSprintPointTotal(Guid sprintId)
        {
            var stories = await _storyService.GetBySprint(sprintId);
            var total = 0;

            foreach(var story in stories)
            {
                var points = story.Points;
                total = total + points;
            }

            return total;
        }

        private async Task<int> GetSprintPointTotal(Guid sprintId, StoryStatus status)
        {
            var stories = await _storyService.GetByStatus(status);
            var storiesByStatus = stories.Where(x => x.SprintId.Equals(sprintId));
            var total = 0;

            foreach (var story in storiesByStatus)
            {
                var points = story.Points;
                total = total + points;
            }

            return total;
        }
    }
}
