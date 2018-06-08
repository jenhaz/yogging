using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using NUnit.Framework;
using Yogging;
using Yogging.Controllers;
using Yogging.Services.Implementations;

namespace Yogging.Tests.Controllers
{
    [TestFixture]
    public class HomeControllerTest
    {
        UserService userService;
        SpotifyService spotifyService;

        [Test]
        public void Index()
        {
            // Arrange
            HomeController controller = new HomeController(userService, spotifyService);

            // Act
            ViewResult result = controller.Index() as ViewResult;

            // Assert
            Assert.IsNotNull(result);
        }
    }
}
