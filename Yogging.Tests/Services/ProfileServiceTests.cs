using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using NSubstitute;
using NUnit.Framework;
using Yogging.Domain.Profiles;
using Yogging.Profiles;
using Yogging.ViewModels;
using ProfileService = Yogging.Profiles.ProfileService;

namespace Yogging.Tests.Services
{
    public class ProfileServiceTests
    {
        private IProfileRepository _profileRepository;
        private IProfileService _subject;
        private Fixture _fixture;

        [SetUp]
        public void Setup()
        {
            _profileRepository = Substitute.For<IProfileRepository>();
            _subject = new ProfileService(_profileRepository);
            _fixture = new Fixture();
        }

        [Test]
        public async Task GetAllProfiles()
        {
            // given
            var profileId = _fixture.Create<Guid>();
            var profile = _fixture.Build<Profile>()
                .With(x => x.Id, profileId)
                .Create();
            var profiles = new List<Profile>
            {
                profile
            };

            _profileRepository.GetAll().Returns(profiles);

            // when
            var result = await _subject.GetAll();

            // then
            await _profileRepository.Received(1).GetAll();
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

        [Test]
        public async Task GetById()
        {
            // given
            var profileId = _fixture.Create<Guid>();
            var profile = _fixture.Build<Profile>()
                .With(x => x.Id, profileId)
                .Create();

            _profileRepository.GetById(profileId).Returns(profile);

            // when
            var result = await _subject.GetById(profileId);

            // then
            await _profileRepository.Received(1).GetById(profileId);
            Assert.That(result, Is.Not.Null);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(profile.Id));
            Assert.That(result.FullName, Is.EqualTo(profile.FullName));
            Assert.That(result.ImageUrl, Is.EqualTo(profile.ImageUrl));
            Assert.That(result.Blurb, Is.EqualTo(profile.Blurb));
            Assert.That(result.LongerBlurb, Is.EqualTo(profile.LongerBlurb));
            Assert.That(result.InstagramUsername, Is.EqualTo(profile.InstagramUsername));
            Assert.That(result.LinkedInUsername, Is.EqualTo(profile.LinkedInUsername));
            Assert.That(result.TwitterUsername, Is.EqualTo(profile.TwitterUsername));
            Assert.That(result.BlogUrl, Is.EqualTo(profile.BlogUrl));
            Assert.That(result.GitHubUsername, Is.EqualTo(profile.GitHubUsername));
            Assert.That(result.CurrentJobTitle, Is.EqualTo(profile.CurrentJobTitle));
            Assert.That(result.ContactEmailAddress, Is.EqualTo(profile.ContactEmailAddress));
        }

        [Test]
        public void Create()
        {
            // given
            var profile = _fixture.Create<ProfileViewModel>();

            // when
            _subject.Create(profile);

            // then
            _profileRepository.Received(1).Create(Arg.Is<Profile>(x => x.Id == profile.Id));
        }

        [Test]
        public void Update()
        {
            // given
            var profile = _fixture.Create<ProfileViewModel>();

            // when
            _subject.Update(profile);

            // then
            _profileRepository.Received(1).Update(Arg.Is<Profile>(x => x.Id == profile.Id));
        }

    }
}