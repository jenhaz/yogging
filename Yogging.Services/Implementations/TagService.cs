﻿using System.Collections.Generic;
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

        public IEnumerable<TagViewModel> GetAllTags()
        {
            var tags = _repository
                .GetTags()
                .Select(GetTag);

            return tags;
        }

        public TagViewModel GetTagById(int? id)
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
