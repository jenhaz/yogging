using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using NSubstitute;
using NUnit.Framework;
using Yogging.DAL.Repository;
using Yogging.Models;
using Yogging.Services.Implementations;
using Yogging.Services.Interfaces;

namespace Yogging.Tests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private IUserRepository _userRepository;
        private IStoryService _storyService;
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _storyService = Substitute.For<IStoryService>();
            _fixture = new Fixture();
        }

        [Test]
        public void GetAllUsers_ReturnsUsers()
        {
            // given
            var userId = _fixture.Create<int>();
            var users = new List<User>
            {
                new User
                {
                    EmailAddress = _fixture.Create<string>(),
                    FirstName = _fixture.Create<string>(),
                    Id = userId,
                    IsInactive = _fixture.Create<bool>(),
                    LastName = _fixture.Create<string>(),
                    Stories = null
                }
            };
            _userRepository.GetUsers().Returns(users);

            // when
            var result = new UserService(_userRepository, _storyService).GetAllUsers().ToList();

            // then
            var actual = result.FirstOrDefault(x => x.Id == userId);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.EmailAddress, Is.EqualTo(users.First().EmailAddress));
            Assert.That(actual.FirstName, Is.EqualTo(users.First().FirstName));
            Assert.That(actual.Id, Is.EqualTo(users.First().Id));
            Assert.That(actual.LastName, Is.EqualTo(users.First().LastName));
        }

        [Test]
        public void GetAllActiveUsers_ReturnsActiveUsers()
        {
            // given
            var activeUserId = _fixture.Create<int>();
            var inactiveUserId = _fixture.Create<int>();
            var users = new List<User>
            {
                new User
                {
                    EmailAddress = _fixture.Create<string>(),
                    FirstName = _fixture.Create<string>(),
                    Id = activeUserId,
                    IsInactive = false,
                    LastName = _fixture.Create<string>(),
                    Stories = null
                },
                new User
                {
                    EmailAddress = _fixture.Create<string>(),
                    FirstName = _fixture.Create<string>(),
                    Id = inactiveUserId,
                    IsInactive = true,
                    LastName = _fixture.Create<string>(),
                    Stories = null
                }
            };
            _userRepository.GetUsers().Returns(users);

            // when
            var result = new UserService(_userRepository, _storyService).GetAllActiveUsers().ToList();

            // then
            Assert.That(result.Count, Is.EqualTo(1));
            var actual = result.FirstOrDefault(x => x.Id == activeUserId);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.EmailAddress, Is.EqualTo(users.First().EmailAddress));
            Assert.That(actual.FirstName, Is.EqualTo(users.First().FirstName));
            Assert.That(actual.Id, Is.EqualTo(users.First().Id));
            Assert.That(actual.LastName, Is.EqualTo(users.First().LastName));
            Assert.That(actual.IsInactive, Is.EqualTo("Active"));
        }

        [Test]
        public void InactiveUser_ReturnsInactive()
        {
            // given
            const bool isInactive = true;

            // when
            var result = new UserService(_userRepository, _storyService).UserIsInactive(isInactive);

            // then
            Assert.AreEqual("Inactive", result);
        }

        [Test]
        public void ActiveUser_ReturnsActive()
        {
            // given
            const bool isInactive = false;

            // when
            var result = new UserService(_userRepository, _storyService).UserIsInactive(isInactive);

            // then
            Assert.AreEqual("Active", result);
        }
    }
}
