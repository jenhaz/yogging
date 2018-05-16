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

        /// <summary>
        /// Get all users from Repository and convert to viewmodel
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserViewModel> GetAllUsers()
        {
            IEnumerable<UserViewModel> users = ContentRepository.GetUsers()
                .Select(x => GetUser(x));

            return users;
        }

        /// <summary>
        /// Get all users that aren't inactive from the Repository and convert to viewmodel
        /// </summary>
        /// <returns></returns>
        public IEnumerable<UserViewModel> GetAllActiveUsers()
        {
            IEnumerable<UserViewModel> users = ContentRepository.GetUsers().Where(y => !y.IsInactive)
                .Select(x => GetUser(x));

            return users;
        }

        /// <summary>
        /// Change bool to a readable string
        /// </summary>
        /// <param name="isInactive">Bool for whether user is inactive or not</param>
        /// <returns></returns>
        private string UserIsInactive (bool isInactive)
        {
            string words = isInactive ? "Inactive" : "Active";

            return words;
        }

        /// <summary>
        /// Convert user to viewmodel
        /// </summary>
        /// <param name="user">User from db</param>
        /// <returns></returns>
        private UserViewModel GetUser(User user)
        {
            return new UserViewModel()
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                EmailAddress = user.EmailAddress,
                IsInactive = UserIsInactive(user.IsInactive),
                Stories = StoryService.GetStoriesByAssignedUser(user.Id)
            };
        }

        /// <summary>
        /// Get all Profiles from the Repository and convert to viewmodel
        /// </summary>
        /// <returns></returns>
        public IEnumerable<ProfileViewModel> GetAllProfiles()
        {
            IEnumerable<ProfileViewModel> profiles = ContentRepository.GetProfiles().Select(x => GetProfile(x));

            return profiles;
        }

        /// <summary>
        /// Convert profile to viewmodel
        /// </summary>
        /// <param name="profile">Profile from db</param>
        /// <returns></returns>
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
                GitHubUsername = profile.GitHubUsername,
                GitHubUrl = !string.IsNullOrEmpty(profile.GitHubUsername) ? "https://www.github.com/" + profile.GitHubUsername : string.Empty,
                CurrentJobTitle = profile.CurrentJobTitle,
                ContactEmailAddress = profile.ContactEmailAddress
            };
        }

        /// <summary>
        /// Convert viewmodel to profile model
        /// </summary>
        /// <param name="vm">Profile viewmodel</param>
        /// <returns></returns>
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
                GitHubUsername = vm.GitHubUsername,
                CurrentJobTitle = vm.CurrentJobTitle,
                ContactEmailAddress = vm.ContactEmailAddress
            };
        }
    }
}