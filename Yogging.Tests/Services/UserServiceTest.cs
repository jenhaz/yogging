using NSubstitute;
using NUnit.Framework;
using Yogging.Services.Interfaces;

namespace Yogging.Tests.Services
{
    [TestFixture]
    public class UserServiceTest
    {
        private IUserService _service;

        [SetUp]
        public void SetUp()
        {
            _service = Substitute.For<IUserService>();
        }

        // TODO: fix tests

        //[Test]
        //public void InactiveUser_ReturnsInactive()
        //{
        //    // given
        //    const bool isInactive = true;

        //    // when
        //    var result = _service.UserIsInactive(isInactive);

        //    // then
        //    Assert.AreEqual("Inactive", result);
        //}

        //[Test]
        //public void ActiveUser_ReturnsActive()
        //{
        //    // given
        //    const bool isInactive = false;

        //    // when
        //    var result = _service.UserIsInactive(isInactive);

        //    // then
        //    Assert.AreEqual("Active", result);
        //}
    }
}
