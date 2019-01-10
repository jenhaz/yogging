using System.Collections.Generic;
using System.Linq;
using Yogging.DAL.Repository;
using Yogging.Models;
using Yogging.Models.Enums;
using Yogging.Models.ViewModels;
using Yogging.Services.Interfaces;

namespace Yogging.Services.Implementations
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

        /// <summary>
        /// Get all Sprints from Repository that aren't closed and convert to list of viewmodels
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SprintViewModel> GetAllActiveSprints()
        {
            var sprints = _repository
                .GetSprints()
                .Where(y => y.Status != SprintStatus.Closed)
                .Select(GetSprint);

            return sprints;
        }

        /// <summary>
        /// Get all Sprints from Repository that are closed and convert to list of viewmodels
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SprintViewModel> GetAllClosedSprints()
        {
            var sprints = _repository
                .GetSprints()
                .Where(y => y.Status == SprintStatus.Closed)
                .Select(GetSprint);

            return sprints;
        }

        /// <summary>
        /// Convert Sprint to viewmodel
        /// </summary>
        /// <param name="sprint">Sprint retrieved from db</param>
        /// <returns></returns>
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

        /// <summary>
        /// Convert viewmodel into model for the db
        /// </summary>
        /// <param name="sprint">The viewmodel</param>
        /// <returns></returns>
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

        /// <summary>
        /// Adds together points for each story in a sprint
        /// </summary>
        /// <param name="sprintId">ID for the particular sprint</param>
        /// <returns></returns>
        private int GetSprintPointTotal(int sprintId)
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

        /// <summary>
        /// Adds together points for each story in a sprint, based on story's status
        /// </summary>
        /// <param name="sprintId">ID for the particular sprint</param>
        /// <param name="status">The status of the stories</param>
        /// <returns></returns>
        private int GetSprintPointTotal(int sprintId, StoryStatus status)
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
