using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using NSubstitute;
using NUnit.Framework;
using Yogging.Domain.Stories;
using Yogging.Services.Stories;
using Yogging.ViewModels;

namespace Yogging.Tests.Services
{
    [TestFixture]
    public class StoryServiceTests
    {
        private Fixture _fixture;
        private IStoryRepository _storyRepository;
        private StoryService _subject;

        [SetUp]
        public void SetUp()
        {
            _fixture = new Fixture();
            _storyRepository = Substitute.For<IStoryRepository>();
            _subject = new StoryService(_storyRepository);
        }

        [Test]
        public async Task GetById()
        {
            // given
            var storyId = _fixture.Create<Guid>();
            var story = _fixture.Build<Story>()
                .With(x => x.Id, storyId)
                .Create();

            _storyRepository.GetById(storyId).Returns(story);

            // when
            var result = await _subject.GetById(storyId);

            // then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(story.Id));
            Assert.That(result.Name, Is.EqualTo(story.Name));
            Assert.That(result.CreatedDate, Is.EqualTo(story.CreatedDate));
            Assert.That(result.LastUpdated, Is.EqualTo(story.LastUpdated));
            Assert.That(result.Priority, Is.EqualTo(story.Priority));
            Assert.That(result.Type, Is.EqualTo(story.Type));
            Assert.That(result.Description, Is.EqualTo(story.Description));
            Assert.That(result.AcceptanceCriteria, Is.EqualTo(story.AcceptanceCriteria));
            Assert.That(result.Points, Is.EqualTo(story.Points));
            Assert.That(result.Status, Is.EqualTo(story.Status));
            Assert.That(result.UserId, Is.EqualTo(story.UserId));
            Assert.That(result.SprintId, Is.EqualTo(story.SprintId));
            Assert.That(result.TagId, Is.EqualTo(story.TagId));
        }

        [Test]
        public async Task GetAllStories()
        {
            // given
            var storyId = _fixture.Create<Guid>();
            var story = _fixture
                .Build<Story>()
                .With(x => x.Id, storyId)
                .Create();

            var stories = new List<Story>
            {
                story
            };

            _storyRepository.GetAll().Returns(stories);

            // when
            var result = await _subject.GetAll();

            // then
            Assert.That(result, Is.Not.Null);
            var actual = result.FirstOrDefault(x => x.Id == storyId);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.AcceptanceCriteria, Is.EqualTo(stories.First().AcceptanceCriteria));
            Assert.That(actual.CreatedDate, Is.EqualTo(stories.First().CreatedDate));
            Assert.That(actual.Description, Is.EqualTo(stories.First().Description));
            Assert.That(actual.Id, Is.EqualTo(stories.First().Id));
            Assert.That(actual.LastUpdated, Is.EqualTo(stories.First().LastUpdated));
            Assert.That(actual.Name, Is.EqualTo(stories.First().Name));
            Assert.That(actual.Points, Is.EqualTo(stories.First().Points));
            Assert.That(actual.Priority, Is.EqualTo(stories.First().Priority));
            Assert.That(actual.Status, Is.EqualTo(stories.First().Status));
        }

        [Test]
        public async Task GetAllStoriesBySprint()
        {
            // given
            var sprintId = _fixture.Create<Guid>();
            var storyInSprintId = _fixture.Create<Guid>();
            var storyInSprint = _fixture
                .Build<Story>()
                .With(x => x.Id, storyInSprintId)
                .With(x => x.SprintId, sprintId)
                .Create();
            
            var stories = new List<Story>
            {
                storyInSprint
            };

            _storyRepository.GetBySprintId(sprintId).Returns(stories);

            // when
            var result = await _subject.GetBySprint(sprintId);

            // then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(1));
            var actual = result.FirstOrDefault(x => x.Id == storyInSprintId);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.AcceptanceCriteria, Is.EqualTo(storyInSprint.AcceptanceCriteria));
            Assert.That(actual.CreatedDate, Is.EqualTo(storyInSprint.CreatedDate));
            Assert.That(actual.Description, Is.EqualTo(storyInSprint.Description));
            Assert.That(actual.Id, Is.EqualTo(storyInSprint.Id));
            Assert.That(actual.LastUpdated, Is.EqualTo(storyInSprint.LastUpdated));
            Assert.That(actual.Name, Is.EqualTo(storyInSprint.Name));
            Assert.That(actual.Points, Is.EqualTo(storyInSprint.Points));
            Assert.That(actual.Priority, Is.EqualTo(storyInSprint.Priority));
            Assert.That(actual.SprintId, Is.EqualTo(storyInSprint.SprintId));
            Assert.That(actual.Status, Is.EqualTo(storyInSprint.Status));
        }

        [Test]
        public async Task GetAllStoriesByTag()
        {
            // given
            var tagId = _fixture.Create<Guid>();
            var storyWithTagId = _fixture.Create<Guid>();
            var storyWithTag = _fixture.Build<Story>()
                .With(x => x.TagId, tagId)
                .With(x => x.Id, storyWithTagId)
                .Create();
            var storyWithoutTagId = _fixture.Create<Guid>();
            var storyWithoutTag = _fixture.Build<Story>()
                .With(x => x.Id, storyWithoutTagId)
                .Create();

            var stories = new List<Story>
            {
                storyWithTag,
                storyWithoutTag
            };

            _storyRepository.GetAll().Returns(stories);

            // when
            var result = await _subject.GetByTag(tagId);

            // then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(1));
            var actual = result.FirstOrDefault(x => x.Id == storyWithTagId);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.AcceptanceCriteria, Is.EqualTo(storyWithTag.AcceptanceCriteria));
            Assert.That(actual.CreatedDate, Is.EqualTo(storyWithTag.CreatedDate));
            Assert.That(actual.Description, Is.EqualTo(storyWithTag.Description));
            Assert.That(actual.Id, Is.EqualTo(storyWithTag.Id));
            Assert.That(actual.LastUpdated, Is.EqualTo(storyWithTag.LastUpdated));
            Assert.That(actual.Name, Is.EqualTo(storyWithTag.Name));
            Assert.That(actual.Points, Is.EqualTo(storyWithTag.Points));
            Assert.That(actual.Priority, Is.EqualTo(storyWithTag.Priority));
            Assert.That(actual.TagId, Is.EqualTo(storyWithTag.TagId));
            Assert.That(actual.Status, Is.EqualTo(storyWithTag.Status));
        }

        [TestCase(StoryStatus.ToDo, StoryStatus.Done)]
        [TestCase(StoryStatus.Done, StoryStatus.InProgress)]
        [TestCase(StoryStatus.InProgress, StoryStatus.ToDo)]
        [TestCase(StoryStatus.ToDo, StoryStatus.InProgress)]
        [TestCase(StoryStatus.Done, StoryStatus.ToDo)]
        [TestCase(StoryStatus.InProgress, StoryStatus.Done)]
        public async Task GetAllStoriesByStatus(
            StoryStatus filterStatus, 
            StoryStatus otherStatus)
        {
            // given
            var storyWithStatusId = _fixture.Create<Guid>();

            var storyWithStatus = _fixture
                .Build<Story>()
                .With(x => x.Id, storyWithStatusId)
                .With(x => x.Status, filterStatus)
                .Create();
            var storyWithoutStatus = _fixture
                .Build<Story>()
                .With(x => x.Status, otherStatus)
                .Create();

            var stories = new List<Story>
            {
                storyWithStatus,
                storyWithoutStatus
            };

            _storyRepository.GetAll().Returns(stories);

            // when
            var result = await _subject.GetByStatus(filterStatus);

            // then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count, Is.EqualTo(1));
            var actual = result.FirstOrDefault(x => x.Id == storyWithStatusId);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.AcceptanceCriteria, Is.EqualTo(storyWithStatus.AcceptanceCriteria));
            Assert.That(actual.CreatedDate, Is.EqualTo(storyWithStatus.CreatedDate));
            Assert.That(actual.Description, Is.EqualTo(storyWithStatus.Description));
            Assert.That(actual.Id, Is.EqualTo(storyWithStatus.Id));
            Assert.That(actual.LastUpdated, Is.EqualTo(storyWithStatus.LastUpdated));
            Assert.That(actual.Name, Is.EqualTo(storyWithStatus.Name));
            Assert.That(actual.Points, Is.EqualTo(storyWithStatus.Points));
            Assert.That(actual.Priority, Is.EqualTo(storyWithStatus.Priority));
            Assert.That(actual.Status, Is.EqualTo(storyWithStatus.Status));
        }

        [Test]
        public async Task MissingTag_ReturnsNull()
        {
            // given
            var storyId = _fixture.Create<Guid>();
            var story = _fixture.Build<Story>()
                .With(x => x.TagId, null)
                .With(x => x.Id, storyId)
                .Create();

            _storyRepository.GetById(storyId).Returns(story);

            // when
            var result = await _subject.GetById(storyId);

            // then
            Assert.AreEqual("#ffffff", result.TagColour);
            Assert.AreEqual(string.Empty, result.TagName);
            Assert.AreEqual(null, result.TagId);
        }

        [Test]
        public async Task MissingUser_ReturnsNull()
        {
            // given
            var storyId = _fixture.Create<Guid>();
            var story = _fixture.Build<Story>()
                .With(x => x.UserId, null)
                .With(x => x.Id, storyId)
                .Create();

            _storyRepository.GetById(storyId).Returns(story);

            // when
            var result = await _subject.GetById(storyId);

            // then
            Assert.AreEqual(null, result.UserId);
            Assert.AreEqual(string.Empty, result.UserName);
        }

        [Test]
        public async Task MissingSprint_ReturnsNull()
        {
            // given
            var storyId = _fixture.Create<Guid>();
            var story = _fixture.Build<Story>()
                .With(x => x.SprintId, null)
                .With(x => x.Id, storyId)
                .Create();

            _storyRepository.GetById(storyId).Returns(story);

            // when
            var result = await _subject.GetById(storyId);

            // then
            Assert.AreEqual(null, result.SprintId);
            Assert.AreEqual(string.Empty, result.SprintName);
        }

        [Test]
        public void Create()
        {
            // given
            var story = _fixture.Create<StoryViewModel>();

            // when
            _subject.Create(story);

            // then
            _storyRepository.Received(1).Create(Arg.Is<Story>(x => x.Id == story.Id));
        }

        [Test]
        public async Task Update()
        {
            // given
            var story = _fixture.Create<StoryViewModel>();

            // when
            await _subject.Update(story);

            // then
            await _storyRepository.Received(1).Update(Arg.Is<Story>(x => x.Id == story.Id));
        }

        [Test]
        public void Delete()
        {
            // given
            var story = _fixture.Create<StoryViewModel>();

            // when
            _subject.Delete(story);

            // then
            _storyRepository.Received(1).Delete(Arg.Is<Story>(x => x.Id == story.Id));
        }
    }
}
