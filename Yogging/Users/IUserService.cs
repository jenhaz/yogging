using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Yogging.ViewModels;

namespace Yogging.Users
{
    public interface IUserService
    {
        Task<IEnumerable<UserViewModel>> GetAll();
        Task<IEnumerable<UserViewModel>> GetActive();
        Task<UserViewModel> GetById(Guid id);
        Task Create(UserViewModel viewModel);
        Task Update(UserViewModel viewModel);
        Task Delete(UserViewModel viewModel);
    }
}