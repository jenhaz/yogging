using System.Collections.Generic;
using Yogging.Domain.Sprints;
using Yogging.ViewModels;

namespace Yogging.Sprints
{
    public interface ISprintService
    {
        IEnumerable<SprintViewModel> GetAllActiveSprints();
        IEnumerable<SprintViewModel> GetAllClosedSprints();
        SprintViewModel GetSprint(Sprint sprint);
        Sprint PutSprint(SprintViewModel sprint);
    }
}
