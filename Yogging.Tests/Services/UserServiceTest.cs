using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yogging.DAL.Repository;
using Yogging.Services.Implementations;

namespace Yogging.Tests.Services
{
    [TestFixture]
    public class UserServiceTest
    {
        ContentRepository contentRepository;
        StoryService storyService;

        [Test]
        public void InactiveUser_ReturnsInactive()
        {
            bool isInactive = true;
            UserService userService = new UserService(contentRepository, storyService);

            string result = userService.UserIsInactive(isInactive);

            Assert.AreEqual("Inactive", result);
        }

        [Test]
        public void ActiveUser_ReturnsActive()
        {
            bool isInactive = false;
            UserService userService = new UserService(contentRepository, storyService);

            string result = userService.UserIsInactive(isInactive);

            Assert.AreEqual("Active", result);
        }
    }
}
