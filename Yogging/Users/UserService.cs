using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<UserViewModel>> GetAll()
        {
            var users = await _userRepository.GetAll();
            var tasks = users.Select(GetViewModel);
            return await Task.WhenAll(tasks);
        }

        public async Task<UserViewModel> GetById(Guid id)
        {
            var user = await _userRepository.GetById(id);
            return await GetViewModel(user);
        }

        public async Task<IEnumerable<UserViewModel>> GetActive()
        {
            var users = await _userRepository.GetAll();
            var tasks = users
                .Where(y => !y.IsInactive)
                .Select(GetViewModel);

            return await Task.WhenAll(tasks);
        }

        public async Task Create(UserViewModel viewModel)
        {
            var user = GetUser(viewModel);
            await _userRepository.Create(user);
        }

        public async Task Update(UserViewModel viewModel)
        {
            var user = GetUser(viewModel);
            await _userRepository.Update(user);
        }

        public async Task Delete(UserViewModel viewModel)
        {
            var user = GetUser(viewModel);
            await _userRepository.Delete(user);
        }

        private static string UserIsInactive(bool isInactive)
        {
            return isInactive ? "Inactive" : "Active";
        }

        private static bool UserIsInactive(string isInactive)
        {
            return isInactive.Equals("Inactive");
        }

        private async Task<UserViewModel> GetViewModel(User user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailAddress = user.EmailAddress,
                IsInactive = UserIsInactive(user.IsInactive),
                Stories = await _storyService.GetByAssignedUser(user.Id)
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