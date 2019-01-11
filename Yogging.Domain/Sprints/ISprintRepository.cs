using System.Collections.Generic;
using Yogging.Models;

namespace Yogging.Domain.Sprints
{
    public interface ISprintRepository
    {
        IEnumerable<Sprint> GetSprints();
    }
}