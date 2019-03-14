using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using NSubstitute;
using NUnit.Framework;
using Yogging.Domain.Users;
using Yogging.Services.Stories;
using Yogging.Services.Users;
using Yogging.ViewModels;

namespace Yogging.Tests.Services
{
    [TestFixture]
    public class UserServiceTests
    {
        private IUserRepository _userRepository;
        private IStoryService _storyService;
        private UserService _subject;
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _storyService = Substitute.For<IStoryService>();
            _subject = new UserService(_userRepository, _storyService);
            _fixture = new Fixture();
        }

        [Test]
        public async Task GetAllUsers()
        {
            // given
            var userId = _fixture.Create<Guid>();
            var user = _fixture.Build<User>()
                .With(x => x.Id, userId)
                .Create();

            _userRepository.GetAll().Returns(new List<User> { user });

            // when
            var result = await _subject.GetAll();

            // then
            var actual = result.FirstOrDefault(x => x.Id == userId);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.EmailAddress, Is.EqualTo(user.EmailAddress));
            Assert.That(actual.FirstName, Is.EqualTo(user.FirstName));
            Assert.That(actual.Id, Is.EqualTo(user.Id));
            Assert.That(actual.LastName, Is.EqualTo(user.LastName));
        }

        [Test]
        public async Task GetAllActiveUsers()
        {
            // given
            var activeUserId = _fixture.Create<Guid>();
            var activeUser = _fixture.Build<User>()
                .With(x => x.Id, activeUserId)
                .With(x => x.IsInactive, false)
                .Create();
            var inactiveUserId = _fixture.Create<Guid>();
            var inactiveUser = _fixture.Build<User>()
                .With(x => x.Id, inactiveUserId)
                .With(x => x.IsInactive, true)
                .Create();

            var users = new List<User>
            {
                activeUser,
                inactiveUser
            };

            _userRepository.GetAll().Returns(users);

            // when
            var result = await _subject.GetActive();

            // then
            Assert.That(result.Count, Is.EqualTo(1));
            var actual = result.FirstOrDefault(x => x.Id == activeUserId);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.EmailAddress, Is.EqualTo(activeUser.EmailAddress));
            Assert.That(actual.FirstName, Is.EqualTo(activeUser.FirstName));
            Assert.That(actual.Id, Is.EqualTo(activeUser.Id));
            Assert.That(actual.LastName, Is.EqualTo(activeUser.LastName));
            Assert.That(actual.IsInactive, Is.EqualTo("Active"));
        }

        [Test]
        public async Task GetById()
        {
            // given
            var userId = _fixture.Create<Guid>();
            var user = _fixture.Build<User>()
                .With(x => x.Id, userId)
                .Create();

            _userRepository.GetById(userId).Returns(user);

            // when
            var result = await _subject.GetById(userId);

            // then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.EmailAddress, Is.EqualTo(user.EmailAddress));
            Assert.That(result.FirstName, Is.EqualTo(user.FirstName));
            Assert.That(result.Id, Is.EqualTo(user.Id));
            Assert.That(result.LastName, Is.EqualTo(user.LastName));
        }

        [Test]
        public async Task InactiveUser_ReturnsInactive()
        {
            // given
            var userId = _fixture.Create<Guid>();
            var user = _fixture.Build<User>()
                .With(x => x.Id, userId)
                .With(x => x.IsInactive, true)
                .Create();

            _userRepository.GetById(userId).Returns(user);

            // when
            var result = await _subject.GetById(userId);

            // then
            Assert.AreEqual("Inactive", result.IsInactive);
        }

        [Test]
        public async Task ActiveUser_ReturnsActive()
        {
            // given
            var userId = _fixture.Create<Guid>();
            var user = _fixture.Build<User>()
                .With(x => x.Id, userId)
                .With(x => x.IsInactive, false)
                .Create();

            _userRepository.GetById(userId).Returns(user);

            // when
            var result = await _subject.GetById(userId);

            // then
            Assert.AreEqual("Active", result.IsInactive);
        }

        [Test]
        public void Create()
        {
            // given
            var user = _fixture.Create<UserViewModel>();

            // when
            _subject.Create(user);

            // then
            _userRepository.Received(1).Create(Arg.Is<User>(x => x.Id == user.Id));
        }

        [Test]
        public void Update()
        {
            // given
            var user = _fixture.Create<UserViewModel>();

            // when
            _subject.Update(user);

            // then
            _userRepository.Received(1).Update(Arg.Is<User>(x => x.Id == user.Id));
        }

        [Test]
        public void Delete()
        {
            // given
            var user = _fixture.Create<UserViewModel>();

            // when
            _subject.Delete(user);

            // then
            _userRepository.Received(1).Delete(Arg.Is<User>(x => x.Id == user.Id));
        }
    }
}
