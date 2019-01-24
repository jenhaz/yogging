using System;
using System.Collections.Generic;
using System.Linq;
using Yogging.Domain.Users;
using Yogging.Stories;
using Yogging.ViewModels;

namespace Yogging.Users
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IStoryService _service;

        public UserService(
            IUserRepository repository, 
            IStoryService service)
        {
            _repository = repository;
            _service = service;
        }

        public IEnumerable<UserViewModel> GetAll()
        {
            var users = _repository
                .GetAll()
                .Select(GetViewModel);

            return users;
        }

        public UserViewModel GetById(Guid id)
        {
            var user = _repository.GetById(id);

            return GetViewModel(user);
        }

        public IEnumerable<UserViewModel> GetActive()
        {
            var users = _repository
                .GetAll()
                .Where(y => !y.IsInactive)
                .Select(GetViewModel);

            return users;
        }

        public void Create(UserViewModel viewModel)
        {
            var user = GetUser(viewModel);
            _repository.Create(user);
        }

        public void Update(UserViewModel viewModel)
        {
            var user = GetUser(viewModel);
            _repository.Update(user);
        }

        public void Delete(UserViewModel viewModel)
        {
            var user = GetUser(viewModel);
            _repository.Delete(user);
        }

        private static string UserIsInactive(bool isInactive)
        {
            var words = isInactive ? "Inactive" : "Active";

            return words;
        }

        private static bool UserIsInactive(string isInactive)
        {
            return isInactive.Equals("Inactive");
        }

        private UserViewModel GetViewModel(User user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailAddress = user.EmailAddress,
                IsInactive = UserIsInactive(user.IsInactive),
                Stories = _service.GetByAssignedUser(user.Id)
            };
        }

        private static User GetUser(UserViewModel viewModel)
        {
            return new User
            {
                Id = viewModel.Id,
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                EmailAddress = viewModel.EmailAddress,
                IsInactive = UserIsInactive(viewModel.IsInactive)
            };
        }
    }
}