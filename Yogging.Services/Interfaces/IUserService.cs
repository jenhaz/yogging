using System.Collections.Generic;
using Yogging.Models.ViewModels;

namespace Yogging.Services.Interfaces
{
    public interface IUserService
    {
        IEnumerable<UserViewModel> GetAllUsers();
        IEnumerable<UserViewModel> GetAllActiveUsers();
        string UserIsInactive(bool isInactive);
    }
}
