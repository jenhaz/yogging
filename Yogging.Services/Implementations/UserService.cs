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

        public IEnumerable<ProfileViewModel> GetAllProfiles()
        {
            var profiles = ContentRepository.GetProfiles().Select(x => GetProfile(x));

            return profiles;
        }

        public ProfileViewModel GetProfile(Profile profile)
        {
            return new ProfileViewModel()
            {
                Id = profile.Id,
                FullName = profile.FullName,
                ImageUrl = profile.ImageUrl,
                Blurb = profile.Blurb,
                LongerBlurb = profile.LongerBlurb,
                InstagramUsername = profile.InstagramUsername,
                InstagramUrl = !string.IsNullOrEmpty(profile.InstagramUsername) ? "https://www.instagram.com/" + profile.InstagramUsername : string.Empty,
                LinkedInUsername = profile.LinkedInUsername,
                LinkedInUrl = !string.IsNullOrEmpty(profile.LinkedInUsername) ? "https://www.linkedin.com/in/" + profile.LinkedInUsername : string.Empty,
                TwitterUsername = profile.TwitterUsername,
                TwitterUrl = !string.IsNullOrEmpty(profile.TwitterUsername) ? "https://www.twitter.com/" + profile.TwitterUsername : string.Empty,
                BlogUrl = profile.BlogUrl,
                CurrentJobTitle = profile.CurrentJobTitle,
                ContactEmailAddress = profile.ContactEmailAddress
            };
        }

        public Profile PutProfile(ProfileViewModel vm)
        {
            return new Profile()
            {
                Id = vm.Id,
                FullName = vm.FullName,
                ImageUrl = vm.ImageUrl,
                Blurb = vm.Blurb,
                LongerBlurb = vm.LongerBlurb,
                InstagramUsername = vm.InstagramUsername,
                LinkedInUsername = vm.LinkedInUsername,
                TwitterUsername = vm.TwitterUsername,
                BlogUrl = vm.BlogUrl,
                CurrentJobTitle = vm.CurrentJobTitle,
                ContactEmailAddress = vm.ContactEmailAddress
            };
        }
    }
}
