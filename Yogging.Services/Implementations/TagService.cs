using System.Collections.Generic;
using System.Linq;
using Yogging.DAL.Context;
using Yogging.DAL.Repository;
using Yogging.Models;
using Yogging.Models.ViewModels;
using Yogging.Services.Interfaces;

namespace Yogging.Services.Implementations
{
    public class TagService : ITagService
    {
        private YoggingContext db = new YoggingContext();
        private IContentRepository ContentRepository { get; }
        private IStoryService StoryService { get; }

        public TagService(IContentRepository contentRepository, IStoryService storyService)
        {
            ContentRepository = contentRepository;
            StoryService = storyService;
        }

        /// <summary>
        /// Get all tags from Repository and convert to viewmodel
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TagViewModel> GetAllTags()
        {
            IEnumerable<TagViewModel> tags = ContentRepository.GetTags()
                .Select(x => GetTag(x));

            return tags;
        }

        /// <summary>
        /// Get Tag by its ID and convert to viewmodel
        /// </summary>
        /// <param name="id">ID of the particular tag</param>
        /// <returns></returns>
        public TagViewModel GetTagById(int? id)
        {
            TagViewModel tag = ContentRepository.GetTags().Where(y => y.Id.Equals(id))
                .Select(x => GetTag(x)).FirstOrDefault();

            return tag;
        }

        /// <summary>
        /// Convert tag to viewmodel
        /// </summary>
        /// <param name="tag">Tag from db</param>
        /// <returns></returns>
        private TagViewModel GetTag(Tag tag)
        {
            return new TagViewModel
            {
                Id = tag.Id,
                Name = tag.Name,
                Stories = StoryService.GetStoriesByTag(tag.Id),
                Colour = tag.Colour
            };
        }
    }
}
