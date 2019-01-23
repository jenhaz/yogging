using System.Collections.Generic;
using Yogging.ViewModels;

namespace Yogging.Users
{
    public interface IUserService
    {
        IEnumerable<UserViewModel> GetAllUsers();
        IEnumerable<UserViewModel> GetAllActiveUsers();
        string UserIsInactive(bool isInactive);
    }
}
