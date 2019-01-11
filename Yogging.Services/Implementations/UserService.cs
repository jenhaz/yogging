using System.Collections.Generic;
using System.Linq;
using Yogging.Domain.Users;
using Yogging.Models;
using Yogging.Models.ViewModels;
using Yogging.Services.Interfaces;

namespace Yogging.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _repository;
        private readonly IStoryService _service;

        public UserService(IUserRepository repository, IStoryService service)
        {
            _repository = repository;
            _service = service;
        }

        public IEnumerable<UserViewModel> GetAllUsers()
        {
            var users = _repository
                .GetUsers()
                .Select(GetUser);

            return users;
        }

        public IEnumerable<UserViewModel> GetAllActiveUsers()
        {
            var users = _repository
                .GetUsers()
                .Where(y => !y.IsInactive)
                .Select(GetUser);

            return users;
        }

        public string UserIsInactive (bool isInactive)
        {
            var words = isInactive ? "Inactive" : "Active";

            return words;
        }

        private UserViewModel GetUser(User user)
        {
            return new UserViewModel
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailAddress = user.EmailAddress,
                IsInactive = UserIsInactive(user.IsInactive),
                Stories = _service.GetStoriesByAssignedUser(user.Id)
            };
        }
    }
}