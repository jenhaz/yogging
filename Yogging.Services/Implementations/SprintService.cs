using System.Collections.Generic;
using System.Linq;
using Yogging.DAL.Context;
using Yogging.DAL.Repository;
using Yogging.Models;
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
        public IEnumerable<SprintViewModel> GetAllSprints()
        {
            var sprints = ContentRepository.GetSprints()
                .Select(x => GetSprint(x));

            return sprints;
        }

        private SprintViewModel GetSprint(Sprint sprint)
        {
            return new SprintViewModel()
            {
                Id = sprint.Id,
                Name = sprint.Name,
                StartDate = sprint.StartDate,
                EndDate = sprint.EndDate,
                Stories = StoryService.GetStoriesBySprint(sprint.Id),
                Status = sprint.Status,
                SprintPointTotal = GetSprintPointTotal(sprint.Id)
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
    }
}
