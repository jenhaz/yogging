using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yogging.Domain.Stories;
using Yogging.ViewModels;

namespace Yogging.Stories
{
    public interface IStoryService
    {
        IEnumerable<StoryViewModel> GetAll();
        StoryViewModel GetById(Guid id);
        IEnumerable<StoryViewModel> GetBySprint(Guid sprintId);
        IEnumerable<StoryViewModel> GetByTag(Guid tagId);
        IEnumerable<StoryViewModel> GetByAssignedUser(Guid userId);
        IEnumerable<StoryViewModel> GetByStatus(StoryStatus status);
        void Create(StoryViewModel viewModel);
        Task Update(StoryViewModel viewModel);
        void Delete(StoryViewModel viewModel);
    }
}
