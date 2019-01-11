using System;
using System.Collections.Generic;
using System.Linq;
using AutoFixture;
using NSubstitute;
using NUnit.Framework;
using Yogging.DAL.Repository;
using Yogging.Models;
using Yogging.Models.Enums;
using Yogging.Services.Implementations;

namespace Yogging.Tests.Services
{
    [TestFixture]
    public class StoryServiceTests
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
        public void GetAllStories_ReturnsAllStories()
        {
            // given
            var storyId = _fixture.Create<int>();
            var stories = new List<Story>
            {
                new Story
                {
                    AcceptanceCriteria = _fixture.Create<string>(),
                    CreatedDate = _fixture.Create<string>(),
                    Description = _fixture.Create<string>(),
                    Id = storyId,
                    LastUpdated = _fixture.Create<string>(),
                    Name = _fixture.Create<string>(),
                    Points = _fixture.Create<int>(),
                    Priority = _fixture.Create<Priority>(),
                    SprintId = null,
                    Status = _fixture.Create<StoryStatus>()
                }
            };
            _storyRepository.GetStories().Returns(stories);

            // when
            var result = new StoryService(_storyRepository).GetAllStories().ToList();

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
            Assert.That(actual.SprintId, Is.Null);
            Assert.That(actual.Status, Is.EqualTo(stories.First().Status));
        }

        [Test]
        public void GetAllStoriesBySprint_ReturnsAllStoriesBySprint()
        {
            // given
            var sprintId = _fixture.Create<int>();
            var storyInSprintId = _fixture.Create<int>();
            var storyNotInSprintId = _fixture.Create<int>();

            var storyInSprint = new Story
            {
                AcceptanceCriteria = _fixture.Create<string>(),
                CreatedDate = _fixture.Create<string>(),
                Description = _fixture.Create<string>(),
                Id = storyInSprintId,
                LastUpdated = _fixture.Create<string>(),
                Name = _fixture.Create<string>(),
                Points = _fixture.Create<int>(),
                Priority = _fixture.Create<Priority>(),
                SprintId = sprintId,
                Status = _fixture.Create<StoryStatus>()
            };
            var storyNotInSprint = new Story
            {
                AcceptanceCriteria = _fixture.Create<string>(),
                CreatedDate = _fixture.Create<string>(),
                Description = _fixture.Create<string>(),
                Id = storyNotInSprintId,
                LastUpdated = _fixture.Create<string>(),
                Name = _fixture.Create<string>(),
                Points = _fixture.Create<int>(),
                Priority = _fixture.Create<Priority>(),
                SprintId = null,
                Status = _fixture.Create<StoryStatus>()
            };

            var stories = new List<Story>
            {
                storyInSprint,
                storyNotInSprint
            };

            _storyRepository.GetStories().Returns(stories);

            // when
            var result = new StoryService(_storyRepository).GetStoriesBySprint(sprintId).ToList();

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
        public void GetAllStoriesByTag_ReturnsAllStoriesByTag()
        {
            // given
            var tagId = _fixture.Create<int>();
            var storyWithTagId = _fixture.Create<int>();
            var storyWithoutTagId = _fixture.Create<int>();

            var storyWithTag = new Story
            {
                AcceptanceCriteria = _fixture.Create<string>(),
                CreatedDate = _fixture.Create<string>(),
                Description = _fixture.Create<string>(),
                Id = storyWithTagId,
                LastUpdated = _fixture.Create<string>(),
                Name = _fixture.Create<string>(),
                Points = _fixture.Create<int>(),
                Priority = _fixture.Create<Priority>(),
                TagId = tagId,
                Status = _fixture.Create<StoryStatus>()
            };
            var storyWithoutTag = new Story
            {
                AcceptanceCriteria = _fixture.Create<string>(),
                CreatedDate = _fixture.Create<string>(),
                Description = _fixture.Create<string>(),
                Id = storyWithoutTagId,
                LastUpdated = _fixture.Create<string>(),
                Name = _fixture.Create<string>(),
                Points = _fixture.Create<int>(),
                Priority = _fixture.Create<Priority>(),
                TagId = null,
                Status = _fixture.Create<StoryStatus>()
            };

            var stories = new List<Story>
            {
                storyWithTag,
                storyWithoutTag
            };

            _storyRepository.GetStories().Returns(stories);

            // when
            var result = new StoryService(_storyRepository).GetStoriesByTag(tagId).ToList();

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
        public void GetAllStoriesByStatus_ReturnsAllStoriesWithStatus(
            StoryStatus filterStatus, 
            StoryStatus otherStatus)
        {
            // given
            var storyWithStatusId = _fixture.Create<int>();

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

            _storyRepository.GetStories().Returns(stories);

            // when
            var result = new StoryService(_storyRepository).GetStoriesByStatus(filterStatus).ToList();

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
        public void MissingTag_ReturnsNull()
        {
            var story = _fixture.Build<Story>()
                .With(x => x.TagId, null)
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
                .With(x => x.UserId, null)
                .Create();

            var result = new StoryService(_storyRepository).GetStory(story);

            Assert.AreEqual(null, result.UserId);
            Assert.AreEqual(string.Empty, result.UserName);
        }

        [Test]
        public void MissingSprint_ReturnsNull()
        {
            var story = _fixture.Build<Story>()
                .With(x => x.SprintId, null)
                .Create();

            var result = new StoryService(_storyRepository).GetStory(story);

            Assert.AreEqual(null, result.SprintId);
            Assert.AreEqual(string.Empty, result.SprintName);
        }
    }
}
