using System;
using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<SprintViewModel> GetAll()
        {
            return _sprintRepository.GetAll().Select(GetViewModel);
        }

        public IEnumerable<SprintViewModel> GetActive()
        {
            var sprints = _sprintRepository
                .GetAll()
                .Where(y => y.Status != SprintStatus.Closed)
                .Select(GetViewModel);

            return sprints;
        }

        public IEnumerable<SprintViewModel> GetClosed()
        {
            var sprints = _sprintRepository
                .GetAll()
                .Where(y => y.Status == SprintStatus.Closed)
                .Select(GetViewModel);

            return sprints;
        }

        public SprintViewModel GetById(Guid id)
        {
            var sprint = _sprintRepository.GetById(id);
            return GetViewModel(sprint);
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

        private SprintViewModel GetViewModel(Sprint sprint)
        {
            return new SprintViewModel
            {
                Id = sprint.Id,
                Name = sprint.Name,
                StartDate = sprint.StartDate,
                EndDate = sprint.EndDate,
                Stories = _storyService.GetBySprint(sprint.Id),
                Status = sprint.Status,
                SprintPointTotal = GetSprintPointTotal(sprint.Id),
                TotalPointsToDo = GetSprintPointTotal(sprint.Id, StoryStatus.ToDo),
                TotalPointsInProgress = GetSprintPointTotal(sprint.Id, StoryStatus.InProgress),
                TotalPointsDone = GetSprintPointTotal(sprint.Id, StoryStatus.Done)
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

        private int GetSprintPointTotal(Guid sprintId)
        {
            var stories = _storyService.GetBySprint(sprintId);
            var total = 0;

            foreach(var story in stories)
            {
                var points = story.Points;
                total = total + points;
            }

            return total;
        }

        private int GetSprintPointTotal(Guid sprintId, StoryStatus status)
        {
            var stories = _storyService.GetByStatus(status)
                .Where(x => x.SprintId.Equals(sprintId));
            var total = 0;

            foreach (var story in stories)
            {
                var points = story.Points;
                total = total + points;
            }

            return total;
        }
    }
}
