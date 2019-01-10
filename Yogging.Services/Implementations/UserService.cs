using System.Collections.Generic;
using System.Linq;
using Yogging.DAL.Repository;
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

        /// <summary>
        /// Get all users from Repository and convert to viewmodel
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserViewModel> GetAllUsers()
        {
            var users = _repository
                .GetUsers()
                .Select(GetUser);

            return users;
        }

        /// <summary>
        /// Get all users that aren't inactive from the Repository and convert to viewmodel
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserViewModel> GetAllActiveUsers()
        {
            var users = _repository
                .GetUsers()
                .Where(y => !y.IsInactive)
                .Select(GetUser);

            return users;
        }

        /// <summary>
        /// Change bool to a readable string
        /// </summary>
        /// <param name="isInactive">Bool for whether user is inactive or not</param>
        /// <returns></returns>
        public string UserIsInactive (bool isInactive)
        {
            var words = isInactive ? "Inactive" : "Active";

            return words;
        }

        /// <summary>
        /// Convert user to viewmodel
        /// </summary>
        /// <param name="user">User from db</param>
        /// <returns></returns>
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