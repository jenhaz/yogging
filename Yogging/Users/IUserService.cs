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
        void Create(UserViewModel viewModel);
        void Update(UserViewModel viewModel);
        void Delete(UserViewModel viewModel);
    }
}