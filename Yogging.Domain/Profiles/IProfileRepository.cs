using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Yogging.Domain.Profiles
{
    public interface IProfileRepository
    {
        Task<IEnumerable<Profile>> GetAll();
        Task<Profile> GetById(Guid id);
        Task Create(Profile profile);
        Task Update(Profile profile);
    }
}