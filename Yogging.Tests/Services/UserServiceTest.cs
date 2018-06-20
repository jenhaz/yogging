using NUnit.Framework;
using Yogging.Services.Implementations;

namespace Yogging.Tests.Services
{
    [TestFixture]
    public class UserServiceTest
    {
        [Test]
        public void InactiveUser_ReturnsInactive()
        {
            bool isInactive = true;
            UserService userService = new UserService(null, null);

            string result = userService.UserIsInactive(isInactive);

            Assert.AreEqual("Inactive", result);
        }

        [Test]
        public void ActiveUser_ReturnsActive()
        {
            bool isInactive = false;
            UserService userService = new UserService(null, null);

            string result = userService.UserIsInactive(isInactive);

            Assert.AreEqual("Active", result);
        }
    }
}
