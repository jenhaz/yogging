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
                Stories = StoryService.GetStoriesBySprint(sprint.Id)
            };
        }
    }
}
