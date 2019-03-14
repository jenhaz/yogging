using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yogging.ViewModels;

namespace Yogging.Services.Tags
{
    public interface ITagService
    {
        Task<IEnumerable<TagViewModel>> GetAll();
        Task<TagViewModel> GetById(Guid id);
        Task Create(TagViewModel viewModel);
        Task Update(TagViewModel viewModel);
        Task Delete(TagViewModel viewModel);
    }
}