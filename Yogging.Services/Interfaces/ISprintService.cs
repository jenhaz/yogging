using System.Collections.Generic;
using Yogging.Models;
using Yogging.Models.ViewModels;

namespace Yogging.Services.Interfaces
{
    public interface ISprintService
    {
        IEnumerable<SprintViewModel> GetAllActiveSprints();
        IEnumerable<SprintViewModel> GetAllExpiredSprints();
        SprintViewModel GetSprint(Sprint sprint);
        Sprint PutSprint(SprintViewModel sprint);
    }
}
