using System.Collections.Generic;
using Yogging.Models;

namespace Yogging.Domain.Profiles
{
    public interface IProfileRepository
    {
        IEnumerable<Profile> GetProfiles();
    }
}