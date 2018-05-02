using System.Collections.Generic;
using Yogging.Models;
using Yogging.Models.ViewModels;

namespace Yogging.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserViewModel> GetAllUsers();
        IEnumerable<UserViewModel> GetAllActiveUsers();
        IEnumerable<ProfileViewModel> GetAllProfiles();
        ProfileViewModel GetProfile(Profile profile);
        Profile PutProfile(ProfileViewModel vm);
    }
}
