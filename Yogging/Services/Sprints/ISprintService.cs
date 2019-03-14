using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yogging.ViewModels;

namespace Yogging.Services.Sprints
{
    public interface ISprintService
    {
        Task<IEnumerable<SprintViewModel>> GetAll();
        Task<IEnumerable<SprintViewModel>> GetActive();
        Task<IEnumerable<SprintViewModel>> GetClosed();
        Task<SprintViewModel> GetById(Guid id);
        Task Create(SprintViewModel viewModel);
        Task Update(SprintViewModel viewModel);
        Task Delete(SprintViewModel viewModel);
    }
}
