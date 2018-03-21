using System.Collections.Generic;
using System.Linq;
using Yogging.DAL.Context;
using Yogging.DAL.Repository;
using Yogging.Models;
using Yogging.Models.ViewModels;
using Yogging.Services.Interfaces;

namespace Yogging.Services.Implementations
{
    public class UserService : IUserService
    {
        private YoggingContext db = new YoggingContext();
        private IContentRepository ContentRepository { get; }
        private IStoryService StoryService { get; }

        public UserService(IContentRepository contentRepository, IStoryService storyService)
        {
            ContentRepository = contentRepository;
            StoryService = storyService;
        }

        public IEnumerable<UserViewModel> GetAllUsers()
        {
            var users = ContentRepository.GetUsers()
                .Select(x => GetUser(x));

            return users;
        }

        public IEnumerable<UserViewModel> GetAllActiveUsers()
        {
            var users = ContentRepository.GetUsers().Where(y => !y.IsInactive)
                .Select(x => GetUser(x));

            return users;
        }

        private UserViewModel GetUser(User user)
        {
            return new UserViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailAddress = user.EmailAddress,
                IsInactive = user.IsInactive,
                Stories = StoryService.GetStoriesByAssignedUser(user.Id)
            };
        }
    }
}
