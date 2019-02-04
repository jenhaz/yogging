using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yogging.ViewModels;

namespace Yogging.Tags
{
    public interface ITagService
    {
        Task<IEnumerable<TagViewModel>> GetAll();
        Task<TagViewModel> GetById(Guid id);
        void Create(TagViewModel viewModel);
        void Update(TagViewModel viewModel);
        void Delete(TagViewModel viewModel);
    }
}