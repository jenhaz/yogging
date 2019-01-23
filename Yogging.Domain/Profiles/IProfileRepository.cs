using System.Collections.Generic;

namespace Yogging.Domain.Profiles
{
    public interface IProfileRepository
    {
        IEnumerable<Profile> GetProfiles();
    }
}