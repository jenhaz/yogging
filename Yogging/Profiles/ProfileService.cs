using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yogging.Domain.Profiles;
using Yogging.ViewModels;

namespace Yogging.Profiles
{
    public class ProfileService : IProfileService
    {
        private readonly IProfileRepository _repository;

        public ProfileService(IProfileRepository repository)
        {
            _repository = repository;
        }

        public async Task<ProfileViewModel> GetById(Guid id)
        {
            var profile = await _repository.GetById(id);
            return GetViewModel(profile);
        }

        public async Task<IEnumerable<ProfileViewModel>> GetAll()
        {
            var profiles = await _repository.GetAll();

            return profiles?.Select(GetViewModel);
        }

        public async Task Create(ProfileViewModel viewModel)
        {
            var profile = GetProfile(viewModel);
            await _repository.Create(profile);
        }

        public async Task Update(ProfileViewModel viewModel)
        {
            var profile = GetProfile(viewModel);
            await _repository.Update(profile);
        }

        private static ProfileViewModel GetViewModel(Profile profile)
        {
            return new ProfileViewModel
            {
                Id = profile.Id,
                FullName = profile.FullName,
                ImageUrl = profile.ImageUrl,
                Blurb = profile.Blurb,
                LongerBlurb = profile.LongerBlurb,
                InstagramUsername = profile.InstagramUsername,
                InstagramUrl = !string.IsNullOrEmpty(profile.InstagramUsername) 
                    ? "https://www.instagram.com/" + profile.InstagramUsername 
                    : string.Empty,
                LinkedInUsername = profile.LinkedInUsername,
                LinkedInUrl = !string.IsNullOrEmpty(profile.LinkedInUsername) 
                    ? "https://www.linkedin.com/in/" + profile.LinkedInUsername 
                    : string.Empty,
                TwitterUsername = profile.TwitterUsername,
                TwitterUrl = !string.IsNullOrEmpty(profile.TwitterUsername) 
                    ? "https://www.twitter.com/" + profile.TwitterUsername 
                    : string.Empty,
                BlogUrl = profile.BlogUrl,
                GitHubUsername = profile.GitHubUsername,
                GitHubUrl = !string.IsNullOrEmpty(profile.GitHubUsername) 
                    ? "https://www.github.com/" + profile.GitHubUsername 
                    : string.Empty,
                CurrentJobTitle = profile.CurrentJobTitle,
                ContactEmailAddress = profile.ContactEmailAddress
            };
        }

        private static Profile GetProfile(ProfileViewModel vm)
        {
            return new Profile
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