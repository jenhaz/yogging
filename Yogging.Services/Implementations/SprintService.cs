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
    public class SprintService : ISprintService
    {
        private YoggingContext db = new YoggingContext();
        private IContentRepository ContentRepository { get; }
        private IStoryService StoryService { get; }

        public SprintService(IContentRepository contentRepository, IStoryService storyService)
        {
            ContentRepository = contentRepository;
            StoryService = storyService;
        }
        /// <summary>
        /// Get all Sprints from Repository that aren't closed and convert to list of viewmodels
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SprintViewModel> GetAllActiveSprints()
        {
            IEnumerable<SprintViewModel> sprints = ContentRepository.GetSprints().Where(y => y.Status != SprintStatus.Closed)
                .Select(x => GetSprint(x));

            return sprints;
        }

        /// <summary>
        /// Get all Sprints from Repository that are closed and convert to list of viewmodels
        /// </summary>
        /// <returns></returns>
        public IEnumerable<SprintViewModel> GetAllClosedSprints()
        {
            IEnumerable<SprintViewModel> sprints = ContentRepository.GetSprints().Where(y => y.Status == SprintStatus.Closed)
                .Select(x => GetSprint(x));

            return sprints;
        }

        /// <summary>
        /// Convert Sprint to viewmodel
        /// </summary>
        /// <param name="sprint">Sprint retrieved from db</param>
        /// <returns></returns>
        public SprintViewModel GetSprint(Sprint sprint)
        {
            return new SprintViewModel()
            {
                Id = sprint.Id,
                Name = sprint.Name,
                StartDate = sprint.StartDate,
                EndDate = sprint.EndDate,
                Stories = StoryService.GetStoriesBySprint(sprint.Id),
                Status = sprint.Status,
                SprintPointTotal = GetSprintPointTotal(sprint.Id),
                TotalPointsToDo = GetTotalPointsByStatus(sprint.Id, StoryStatus.ToDo),
                TotalPointsInProgress = GetTotalPointsByStatus(sprint.Id, StoryStatus.InProgress),
                TotalPointsDone = GetTotalPointsByStatus(sprint.Id, StoryStatus.Done)
            };
        }

        /// <summary>
        /// Convert viewmodel into model for the db
        /// </summary>
        /// <param name="sprint">The viewmodel</param>
        /// <returns></returns>
        public Sprint PutSprint(SprintViewModel sprint)
        {
            return new Sprint()
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
            IEnumerable<StoryViewModel> stories = StoryService.GetStoriesBySprint(sprintId);
            int total = 0;

            foreach(var story in stories)
            {
                int points = story.Points;
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
        private int GetTotalPointsByStatus(int sprintId, StoryStatus status)
        {
            IEnumerable<StoryViewModel> stories = StoryService.GetStoriesByStatus(status)
                .Where(x => x.SprintId.Equals(sprintId));
            int total = 0;

            foreach (var story in stories)
            {
                int points = story.Points;
                total = total + points;
            }

            return total;
        }
    }
}
