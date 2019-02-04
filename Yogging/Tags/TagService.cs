using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yogging.Domain.Tags;
using Yogging.ViewModels;

namespace Yogging.Tags
{
    public class TagService : ITagService
    {
        private readonly ITagRepository _repository;

        public TagService(ITagRepository repository)
        {
            _repository = repository;
        }

        public async Task<IEnumerable<TagViewModel>> GetAll()
        {
            var tags = await _repository.GetAll();
            return tags.Select(GetViewModel);
        }

        public async Task<TagViewModel> GetById(Guid id)
        {
            var tag = await _repository.GetById(id);
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
