using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using NSubstitute;
using NUnit.Framework;
using Yogging.Domain.Users;
using Yogging.Stories;
using Yogging.Users;
using Yogging.ViewModels;

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
            var userId = _fixture.Create<Guid>();
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
            _userRepository.GetAll().Returns(users);

            // when
            var result = new UserService(_userRepository, _storyService).GetAll().ToList();

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
            var activeUserId = _fixture.Create<Guid>();
            var inactiveUserId = _fixture.Create<Guid>();
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
            _userRepository.GetAll().Returns(users);

            // when
            var result = new UserService(_userRepository, _storyService).GetActive().ToList();

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
            var userId = _fixture.Create<Guid>();
            var user = _fixture.Build<User>()
                .With(x => x.Id, userId)
                .With(x => x.IsInactive, true)
                .Create();

            _userRepository.GetById(userId).Returns(user);

            // when
            var result = new UserService(_userRepository, _storyService).GetById(userId);

            // then
            Assert.AreEqual("Inactive", result.IsInactive);
        }

        [Test]
        public void ActiveUser_ReturnsActive()
        {
            // given
            var userId = _fixture.Create<Guid>();
            var user = _fixture.Build<User>()
                .With(x => x.Id, userId)
                .With(x => x.IsInactive, false)
                .Create();

            _userRepository.GetById(userId).Returns(user);

            // when
            var result = new UserService(_userRepository, _storyService).GetById(userId);

            // then
            Assert.AreEqual("Active", result.IsInactive);
        }
    }
}
