using System;
using System.Collections.Generic;
using Yogging.ViewModels;

namespace Yogging.Sprints
{
    public interface ISprintService
    {
        IEnumerable<SprintViewModel> GetAll();
        IEnumerable<SprintViewModel> GetActive();
        IEnumerable<SprintViewModel> GetClosed();
        SprintViewModel GetById(Guid id);
        void Create(SprintViewModel viewModel);
        void Update(SprintViewModel viewModel);
        void Delete(SprintViewModel viewModel);
    }
}
