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

        public TagService(
            ITagRepository repository, 
            IStoryService service)
        {
            _repository = repository;
            _service = service;
        }

        public IEnumerable<TagViewModel> GetAll()
        {
            var tags = _repository
                .GetAll()
                .Select(GetViewModel);

            return tags;
        }

        public TagViewModel GetById(Guid id)
        {
            var tag = _repository.GetById(id);

            return GetViewModel(tag);
        }

        public void Create(TagViewModel viewModel)
        {
            var tag = GetTag(viewModel);
            _repository.Create(tag);
        }

        public void Update(TagViewModel viewModel)
        {
            var tag = GetTag(viewModel);
            _repository.Update(tag);
        }

        public void Delete(TagViewModel viewModel)
        {
            var tag = GetTag(viewModel);
            _repository.Delete(tag);
        }

        private static TagViewModel GetViewModel(Tag tag)
        {
            return new TagViewModel
            {
                Id = tag.Id,
                Name = tag.Name,
                Colour = tag.Colour
            };
        }

        private static Tag GetTag(TagViewModel viewModel)
        {
            return new Tag
            {
                Id = viewModel.Id,
                Name = viewModel.Name,
                Colour = viewModel.Colour
            };
        }
    }
}
