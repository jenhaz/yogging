using System;
using System.Collections.Generic;
using Yogging.ViewModels;

namespace Yogging.Users
{
    public interface IUserService
    {
        IEnumerable<UserViewModel> GetAll();
        IEnumerable<UserViewModel> GetActive();
        UserViewModel GetById(Guid id);
        void Create(UserViewModel viewModel);
        void Update(UserViewModel viewModel);
        void Delete(UserViewModel viewModel);
    }
}