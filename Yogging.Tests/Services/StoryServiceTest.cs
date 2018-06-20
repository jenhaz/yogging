using AutoFixture;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yogging.Models;
using Yogging.Models.ViewModels;
using Yogging.Services.Implementations;

namespace Yogging.Tests.Services
{
    [TestFixture]
    public class StoryServiceTest
    {
        StoryService storyService = new StoryService(null);

        [Test]
        public void MissingTag_ReturnsNull()
        {
            Fixture fixture = new Fixture();
            Story story = fixture.Build<Story>()
                .With(x => x.Sprint, new Sprint())
                .With(x => x.User, new User())
                .With(x => x.Tag, null)
                .Create();

            StoryViewModel result = storyService.GetStory(story);

            Assert.AreEqual(result.TagColour, "#ffffff");
            Assert.AreEqual(result.TagName, string.Empty);
            Assert.AreEqual(result.TagId, null);
        }

        [Test]
        public void MissingUser_ReturnsNull()
        {
            Fixture fixture = new Fixture();
            Story story = fixture.Build<Story>()
                .With(x => x.Sprint, new Sprint())
                .With(x => x.User, null)
                .With(x => x.Tag, new Tag())
                .Create();

            StoryViewModel result = storyService.GetStory(story);

            Assert.AreEqual(result.UserId, null);
            Assert.AreEqual(result.UserName, string.Empty);
        }

        [Test]
        public void MissingSprint_ReturnsNull()
        {
            Fixture fixture = new Fixture();
            Story story = fixture.Build<Story>()
                .With(x => x.Sprint, null)
                .With(x => x.User, new User())
                .With(x => x.Tag, new Tag())
                .Create();

            StoryViewModel result = storyService.GetStory(story);

            Assert.AreEqual(result.SprintId, null);
            Assert.AreEqual(result.SprintName, string.Empty);
        }
    }
}
