using System.Collections.Generic;

namespace Yogging.Domain.Sprints
{
    public interface ISprintRepository
    {
        IEnumerable<Sprint> GetSprints();
    }
}