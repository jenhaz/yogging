using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using NSubstitute;
using NUnit.Framework;
using Yogging.Domain.Profiles;
using ProfileService = Yogging.Profiles.ProfileService;

namespace Yogging.Tests.Services
{
    public class ProfileServiceTests
    {
        private IProfileRepository _profileRepository;
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _profileRepository = Substitute.For<IProfileRepository>();
            _fixture = new Fixture();
        }

        [Test]
        public void GetAllProfiles_ReturnsProfiles()
        {
            // given
            var profileId = _fixture.Create<Guid>();
            var profile = _fixture.Build<Profile>().With(x => x.Id, profileId).Create();
            var profiles = new List<Profile>
            {
                profile
            };

            _profileRepository.GetProfiles().Returns(profiles);

            // when
            var result = new ProfileService(_profileRepository).GetAllProfiles().ToList();

            // then
            Assert.That(result, Is.Not.Null);
            var actual = result.FirstOrDefault(x => x.Id == profileId);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.EqualTo(profile.Id));
            Assert.That(actual.FullName, Is.EqualTo(profile.FullName));
            Assert.That(actual.ImageUrl, Is.EqualTo(profile.ImageUrl));
            Assert.That(actual.Blurb, Is.EqualTo(profile.Blurb));
            Assert.That(actual.LongerBlurb, Is.EqualTo(profile.LongerBlurb));
            Assert.That(actual.InstagramUsername, Is.EqualTo(profile.InstagramUsername));
            Assert.That(actual.LinkedInUsername, Is.EqualTo(profile.LinkedInUsername));
            Assert.That(actual.TwitterUsername, Is.EqualTo(profile.TwitterUsername));
            Assert.That(actual.BlogUrl, Is.EqualTo(profile.BlogUrl));
            Assert.That(actual.GitHubUsername, Is.EqualTo(profile.GitHubUsername));
            Assert.That(actual.CurrentJobTitle, Is.EqualTo(profile.CurrentJobTitle));
            Assert.That(actual.ContactEmailAddress, Is.EqualTo(profile.ContactEmailAddress));
        }
    }
}