using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yogging.Domain.Stories;
using Yogging.ViewModels;

namespace Yogging.Stories
{
    public interface IStoryService
    {
        Task<IEnumerable<StoryViewModel>> GetAll();
        Task<StoryViewModel> GetById(Guid id);
        Task<IEnumerable<StoryViewModel>> GetBySprint(Guid sprintId);
        Task<IEnumerable<StoryViewModel>> GetByTag(Guid tagId);
        Task<IEnumerable<StoryViewModel>> GetByAssignedUser(Guid userId);
        Task<IEnumerable<StoryViewModel>> GetByStatus(StoryStatus status);
        Task Create(StoryViewModel viewModel);
        Task Update(StoryViewModel viewModel);
        Task Delete(StoryViewModel viewModel);
    }
}
