using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yogging.ViewModels;

namespace Yogging.Profiles
{
    public interface IProfileService
    {
        Task<ProfileViewModel> GetById(Guid id);
        Task<IEnumerable<ProfileViewModel>> GetAll();
        void Create(ProfileViewModel profile);
        void Update(ProfileViewModel profile);
    }
}