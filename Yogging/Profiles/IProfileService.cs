using System;
using System.Collections.Generic;
using Yogging.Domain.Profiles;
using Yogging.ViewModels;

namespace Yogging.Profiles
{
    public interface IProfileService
    {
        IEnumerable<ProfileViewModel> GetAll();
        ProfileViewModel GetById(Guid id);
        void Create(ProfileViewModel profile);
        void Update(ProfileViewModel profile);
    }
}