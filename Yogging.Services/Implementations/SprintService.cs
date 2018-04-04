using System;
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
        public IEnumerable<SprintViewModel> GetAllActiveSprints()
        {
            //var sprints = ContentRepository.GetSprints().Where(y => y.EndDate >= DateTime.Now)
            var sprints = ContentRepository.GetSprints().Where(y => y.Status != SprintStatus.Closed)
                .Select(x => GetSprint(x));

            return sprints;
        }

        public IEnumerable<SprintViewModel> GetAllExpiredSprints()
        {
            //var sprints = ContentRepository.GetSprints().Where(y => y.EndDate < DateTime.Now)
            var sprints = ContentRepository.GetSprints().Where(y => y.Status == SprintStatus.Closed)
                .Select(x => GetSprint(x));

            return sprints;
        }

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
                TotalPointsToDo = GetTotalPointsToDo(sprint.Id),
                TotalPointsInProgress = GetTotalPointsInProgress(sprint.Id),
                TotalPointsDone = GetTotalPointsDone(sprint.Id)
            };
        }

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

        private int GetSprintPointTotal(int sprintId)
        {
            var stories = StoryService.GetStoriesBySprint(sprintId);
            int total = 0;

            foreach(var story in stories)
            {
                int points = story.Points;
                total = total + points;
            }

            return total;
        }

        private int GetTotalPointsToDo(int sprintId)
        {
            var stories = StoryService.GetStoriesByStatus(StoryStatus.ToDo)
                .Where(x => x.SprintId.Equals(sprintId));
            int total = 0;

            foreach (var story in stories)
            {
                int points = story.Points;
                total = total + points;
            }

            return total;
        }

        private int GetTotalPointsInProgress(int sprintId)
        {
            var stories = StoryService.GetStoriesByStatus(StoryStatus.InProgress)
                .Where(x => x.SprintId.Equals(sprintId));
            int total = 0;

            foreach (var story in stories)
            {
                int points = story.Points;
                total = total + points;
            }

            return total;
        }

        private int GetTotalPointsDone(int sprintId)
        {
            var stories = StoryService.GetStoriesByStatus(StoryStatus.Done)
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
