using System.Collections.Generic;
using Yogging.Domain.Profiles;
using Yogging.ViewModels;

namespace Yogging.Profiles
{
    public interface IProfileService
    {
        IEnumerable<ProfileViewModel> GetAllProfiles();
        ProfileViewModel GetProfile(Profile profile);
        Profile PutProfile(ProfileViewModel vm);
    }
}