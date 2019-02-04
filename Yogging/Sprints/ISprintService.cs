using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yogging.ViewModels;

namespace Yogging.Sprints
{
    public interface ISprintService
    {
        Task<IEnumerable<SprintViewModel>> GetAll();
        Task<IEnumerable<SprintViewModel>> GetActive();
        Task<IEnumerable<SprintViewModel>> GetClosed();
        Task<SprintViewModel> GetById(Guid id);
        void Create(SprintViewModel viewModel);
        void Update(SprintViewModel viewModel);
        void Delete(SprintViewModel viewModel);
    }
}
