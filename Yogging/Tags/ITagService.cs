using System;
using System.Collections.Generic;
using Yogging.ViewModels;

namespace Yogging.Tags
{
    public interface ITagService
    {
        IEnumerable<TagViewModel> GetAll();
        TagViewModel GetById(Guid id);
        void Create(TagViewModel viewModel);
        void Update(TagViewModel viewModel);
        void Delete(TagViewModel viewModel);
    }
}