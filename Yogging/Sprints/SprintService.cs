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
        private readonly ISprintRepository _repository;
        private readonly IStoryService _service;

        public SprintService(
            ISprintRepository repository, 
            IStoryService service)
        {
            _repository = repository;
            _service = service;
        }

        public IEnumerable<SprintViewModel> GetAll()
        {
            return _repository.GetAll().Select(GetViewModel);
        }

        public IEnumerable<SprintViewModel> GetActive()
        {
            var sprints = _repository
                .GetAll()
                .Where(y => y.Status != SprintStatus.Closed)
                .Select(GetViewModel);

            return sprints;
        }

        public IEnumerable<SprintViewModel> GetClosed()
        {
            var sprints = _repository
                .GetAll()
                .Where(y => y.Status == SprintStatus.Closed)
                .Select(GetViewModel);

            return sprints;
        }

        public SprintViewModel GetById(Guid id)
        {
            var sprint = _repository.GetById(id);
            return GetViewModel(sprint);
        }

        public void Create(SprintViewModel viewModel)
        {
            var sprint = GetSprint(viewModel);
            _repository.Create(sprint);
        }

        public void Update(SprintViewModel viewModel)
        {
            var sprint = GetSprint(viewModel);
            _repository.Update(sprint);
        }

        public void Delete(SprintViewModel viewModel)
        {
            var sprint = GetSprint(viewModel);
            _repository.Delete(sprint);
        }

        private SprintViewModel GetViewModel(Sprint sprint)
        {
            return new SprintViewModel
            {
                Id = sprint.Id,
                Name = sprint.Name,
                StartDate = sprint.StartDate,
                EndDate = sprint.EndDate,
                Stories = _service.GetBySprint(sprint.Id),
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
            var stories = _service.GetBySprint(sprintId);
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
            var stories = _service.GetByStatus(status)
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
