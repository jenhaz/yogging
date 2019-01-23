using System;
using System.Collections.Generic;

namespace Yogging.Domain.Profiles
{
    public interface IProfileRepository
    {
        IEnumerable<Profile> GetAll();
        Profile GetById(Guid id);
        void Create(Profile profile);
        void Update(Profile profile);
    }
}