using AutoFixture;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yogging.Models;
using Yogging.Services.Implementations;

namespace Yogging.Tests.Services
{
    [TestFixture]
    public class StoryServiceTest
    {
        [Test]
        public void MissingTag_ReturnsNull()
        {
            StoryService storyService = new StoryService(null);

            var fixture = new Fixture();
            var story = fixture.Build<Story>()
                .With(x => x.Sprint, new Sprint())
                .With(x => x.User, new User())
                .With(x => x.Tag, null)
                .Create();

            var result = storyService.GetStory(story);

            Assert.AreEqual(result.TagColour, "#ffffff");
            Assert.AreEqual(result.TagName, string.Empty);
            Assert.AreEqual(result.TagId, null);
        }
    }
}
