using System.Collections.Generic;
using Yogging.Models;
using Yogging.Models.ViewModels;

namespace Yogging.Services.Interfaces
{
    public interface IProfileService
    {
        IEnumerable<ProfileViewModel> GetAllProfiles();
        ProfileViewModel GetProfile(Profile profile);
        Profile PutProfile(ProfileViewModel vm);
    }
}