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
        private readonly IUserRepository _userRepository;
        private readonly IStoryService _storyService;

        public UserService(
            IUserRepository userRepository, 
            IStoryService storyService)
        {
            _userRepository = userRepository;
            _storyService = storyService;
        }

        public IEnumerable<UserViewModel> GetAll()
        {
            var users = _userRepository
                .GetAll()
                .Select(GetViewModel);

            return users;
        }

        public UserViewModel GetById(Guid id)
        {
            var user = _userRepository.GetById(id);

            return GetViewModel(user);
        }

        public IEnumerable<UserViewModel> GetActive()
        {
            var users = _userRepository
                .GetAll()
                .Where(y => !y.IsInactive)
                .Select(GetViewModel);

            return users;
        }

        public void Create(UserViewModel viewModel)
        {
            var user = GetUser(viewModel);
            _userRepository.Create(user);
        }

        public void Update(UserViewModel viewModel)
        {
            var user = GetUser(viewModel);
            _userRepository.Update(user);
        }

        public void Delete(UserViewModel viewModel)
        {
            var user = GetUser(viewModel);
            _userRepository.Delete(user);
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
                Stories = _storyService.GetByAssignedUser(user.Id)
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