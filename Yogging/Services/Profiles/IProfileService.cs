using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yogging.ViewModels;

namespace Yogging.Services.Profiles
{
    public interface IProfileService
    {
        Task<ProfileViewModel> GetById(Guid id);
        Task<IEnumerable<ProfileViewModel>> GetAll();
        Task Create(ProfileViewModel profile);
        Task Update(ProfileViewModel profile);
    }
}