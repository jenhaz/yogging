using System.Collections.Generic;
using Yogging.Models;

namespace Yogging.DAL.Repository
{
    public interface ISprintRepository
    {
        IEnumerable<Sprint> GetSprints();
    }
}