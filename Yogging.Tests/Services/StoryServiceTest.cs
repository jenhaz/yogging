using AutoFixture;
using NSubstitute;
using NUnit.Framework;
using Yogging.DAL.Repository;
using Yogging.Models;
using Yogging.Services.Implementations;

namespace Yogging.Tests.Services
{
    [TestFixture]
    public class StoryServiceTest
    {
        private Fixture _fixture;
        private IStoryRepository _storyRepository;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
            _storyRepository = Substitute.For<IStoryRepository>();
        }

        [Test]
        public void MissingTag_ReturnsNull()
        {
            var story = _fixture.Build<Story>()
                .With(x => x.Sprint, new Sprint())
                .With(x => x.User, new User())
                .With(x => x.Tag, null)
                .Create();

            var result = new StoryService(_storyRepository).GetStory(story);

            Assert.AreEqual("#ffffff", result.TagColour);
            Assert.AreEqual(string.Empty, result.TagName);
            Assert.AreEqual(null, result.TagId);
        }

        [Test]
        public void MissingUser_ReturnsNull()
        {
            var story = _fixture.Build<Story>()
                .With(x => x.Sprint, new Sprint())
                .With(x => x.User, null)
                .With(x => x.Tag, new Tag())
                .Create();

            var result = new StoryService(_storyRepository).GetStory(story);

            Assert.AreEqual(null, result.UserId);
            Assert.AreEqual(string.Empty, result.UserName);
        }

        [Test]
        public void MissingSprint_ReturnsNull()
        {
            var story = _fixture.Build<Story>()
                .With(x => x.Sprint, null)
                .With(x => x.User, new User())
                .With(x => x.Tag, new Tag())
                .Create();

            var result = new StoryService(_storyRepository).GetStory(story);

            Assert.AreEqual(null, result.SprintId);
            Assert.AreEqual(string.Empty, result.SprintName);
        }
    }
}
