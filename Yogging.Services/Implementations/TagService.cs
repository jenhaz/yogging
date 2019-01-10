using System.Collections.Generic;
using System.Linq;
using Yogging.DAL.Repository;
using Yogging.Models;
using Yogging.Models.ViewModels;
using Yogging.Services.Interfaces;

namespace Yogging.Services.Implementations
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _repository;
        private readonly IStoryService _service;

        public TagService(ITagRepository repository, IStoryService service)
        {
            _repository = repository;
            _service = service;
        }

        /// <summary>
        /// Get all tags from Repository and convert to viewmodel
        /// </summary>
        /// <returns></returns>
        public IEnumerable<TagViewModel> GetAllTags()
        {
            var tags = _repository
                .GetTags()
                .Select(GetTag);

            return tags;
        }

        /// <summary>
        /// Get Tag by its ID and convert to viewmodel
        /// </summary>
        /// <param name="id">ID of the particular tag</param>
        /// <returns></returns>
        public TagViewModel GetTagById(int? id)
        {
            var tag = _repository
                .GetTags()
                .Where(y => y.Id.Equals(id))
                .Select(GetTag)
                .FirstOrDefault();

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
                Stories = _service.GetStoriesByTag(tag.Id),
                Colour = tag.Colour
            };
        }
    }
}
