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

        public IEnumerable<SprintViewModel> GetAllActiveSprints()
        {
            var sprints = _repository
                .GetSprints()
                .Where(y => y.Status != SprintStatus.Closed)
                .Select(GetSprint);

            return sprints;
        }

        public IEnumerable<SprintViewModel> GetAllClosedSprints()
        {
            var sprints = _repository
                .GetSprints()
                .Where(y => y.Status == SprintStatus.Closed)
                .Select(GetSprint);

            return sprints;
        }

        public SprintViewModel GetSprint(Sprint sprint)
        {
            return new SprintViewModel
            {
                Id = sprint.Id,
                Name = sprint.Name,
                StartDate = sprint.StartDate,
                EndDate = sprint.EndDate,
                Stories = _service.GetStoriesBySprint(sprint.Id),
                Status = sprint.Status,
                SprintPointTotal = GetSprintPointTotal(sprint.Id),
                TotalPointsToDo = GetSprintPointTotal(sprint.Id, StoryStatus.ToDo),
                TotalPointsInProgress = GetSprintPointTotal(sprint.Id, StoryStatus.InProgress),
                TotalPointsDone = GetSprintPointTotal(sprint.Id, StoryStatus.Done)
            };
        }

        public Sprint PutSprint(SprintViewModel sprint)
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
            var stories = _service.GetStoriesBySprint(sprintId);
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
            var stories = _service.GetStoriesByStatus(status)
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
