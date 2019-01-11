using NSubstitute;
using NUnit.Framework;
using Yogging.DAL.Repository;
using Yogging.Services.Implementations;
using Yogging.Services.Interfaces;

namespace Yogging.Tests.Services
{
    [TestFixture]
    public class UserServiceTest
    {
        private IUserRepository _userRepository;
        private IStoryService _storyService;

        [SetUp]
        public void SetUp()
        {
            _userRepository = Substitute.For<IUserRepository>();
            _storyService = Substitute.For<IStoryService>();
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
