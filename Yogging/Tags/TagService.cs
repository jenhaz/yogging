using System;
using System.Collections.Generic;
using System.Linq;
using Yogging.Domain.Tags;
using Yogging.Stories;
using Yogging.ViewModels;

namespace Yogging.Tags
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

        public IEnumerable<TagViewModel> GetAllTags()
        {
            var tags = _repository
                .GetTags()
                .Select(GetTag);

            return tags;
        }

        public TagViewModel GetTagById(Guid? id)
        {
            var tag = _repository
                .GetTags()
                .Where(y => y.Id.Equals(id))
                .Select(GetTag)
                .FirstOrDefault();

            return tag;
        }

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
