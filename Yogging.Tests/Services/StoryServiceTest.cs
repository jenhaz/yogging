using AutoFixture;
using NSubstitute;
using NUnit.Framework;
using Yogging.Models;
using Yogging.Services.Interfaces;

namespace Yogging.Tests.Services
{
    [TestFixture]
    public class StoryServiceTest
    {
        private IStoryService _storyService;
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _storyService = Substitute.For<IStoryService>();
            _fixture = new Fixture();
        }

        // TODO: fix tests

        //[Test]
        //public void MissingTag_ReturnsNull()
        //{
        //    var story = _fixture.Build<Story>()
        //        .With(x => x.Sprint, new Sprint())
        //        .With(x => x.User, new User())
        //        .With(x => x.Tag, null)
        //        .Create();
            
        //    var result = _storyService.GetStory(story);

        //    Assert.AreEqual(result.TagColour, "#ffffff");
        //    Assert.AreEqual(result.TagName, string.Empty);
        //    Assert.AreEqual(result.TagId, null);
        //}

        //[Test]
        //public void MissingUser_ReturnsNull()
        //{
        //    var story = _fixture.Build<Story>()
        //        .With(x => x.Sprint, new Sprint())
        //        .With(x => x.User, null)
        //        .With(x => x.Tag, new Tag())
        //        .Create();

        //    var result = _storyService.GetStory(story);

        //    Assert.AreEqual(result.UserId, null);
        //    Assert.AreEqual(result.UserName, string.Empty);
        //}

        //[Test]
        //public void MissingSprint_ReturnsNull()
        //{
        //    var user = _fixture.Create<User>();
        //    var tag = _fixture.Create<Tag>();

        //    var story = _fixture.Build<Story>()
        //        .With(x => x.Sprint, null)
        //        .With(x => x.User, user)
        //        .With(x => x.Tag, tag)
        //        .Create();

        //    var result = _storyService.GetStory(story);

        //    Assert.AreEqual(result.SprintId, null);
        //    Assert.AreEqual(result.SprintName, string.Empty);
        //}
    }
}
