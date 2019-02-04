using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoFixture;
using NSubstitute;
using NUnit.Framework;
using Yogging.Domain.Sprints;
using Yogging.Domain.Stories;
using Yogging.Sprints;
using Yogging.Stories;
using Yogging.ViewModels;

namespace Yogging.Tests.Services
{
    public class SprintServiceTests
    {
        private ISprintRepository _sprintRepository;
        private IStoryService _storyService;
        private ISprintService _subject;
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _sprintRepository = Substitute.For<ISprintRepository>();
            _storyService = Substitute.For<IStoryService>();
            _subject = new SprintService(_sprintRepository, _storyService);
            _fixture = new Fixture();
        }

        [Test]
        public async Task GetAllActiveSprints()
        {
            // given
            var activeSprintId = _fixture.Create<Guid>();
            var activeSprint = _fixture
                .Build<Sprint>()
                .With(x => x.Id, activeSprintId)
                .With(x => x.Status, SprintStatus.Open)
                .Create();
            var closedSprint = _fixture
                .Build<Sprint>()
                .With(x => x.Status, SprintStatus.Closed)
                .Create();
            var sprints = new List<Sprint>
            {
                activeSprint,
                closedSprint
            };

            _sprintRepository.GetAll().Returns(sprints);

            // when
            var result = await _subject.GetActive();

            // then
            await _sprintRepository.Received(1).GetAll();
            Assert.That(result.Count, Is.EqualTo(1));
            var actual = result.FirstOrDefault(x => x.Id == activeSprintId);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.EqualTo(activeSprint.Id));
            Assert.That(actual.Name, Is.EqualTo(activeSprint.Name));
            Assert.That(actual.StartDate, Is.EqualTo(activeSprint.StartDate));
            Assert.That(actual.EndDate, Is.EqualTo(activeSprint.EndDate));
            Assert.That(actual.Status, Is.EqualTo(activeSprint.Status));
        }

        [Test]
        public async Task GetAllClosedSprints()
        {
            // given
            var closedSprintId = _fixture.Create<Guid>();
            var activeSprint = _fixture
                .Build<Sprint>()
                .With(x => x.Status, SprintStatus.Open)
                .Create();
            var closedSprint = _fixture
                .Build<Sprint>()
                .With(x => x.Id, closedSprintId)
                .With(x => x.Status, SprintStatus.Closed)
                .Create();
            var sprints = new List<Sprint>
            {
                activeSprint,
                closedSprint
            };

            _sprintRepository.GetAll().Returns(sprints);

            // when
            var result = await _subject.GetClosed();

            // then
            await _sprintRepository.Received(1).GetAll();
            Assert.That(result.Count, Is.EqualTo(1));
            var actual = result.FirstOrDefault(x => x.Id == closedSprintId);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.EqualTo(closedSprint.Id));
            Assert.That(actual.Name, Is.EqualTo(closedSprint.Name));
            Assert.That(actual.StartDate, Is.EqualTo(closedSprint.StartDate));
            Assert.That(actual.EndDate, Is.EqualTo(closedSprint.EndDate));
            Assert.That(actual.Status, Is.EqualTo(closedSprint.Status));
        }

        [Test]
        public async Task GetById()
        {
            // given
            var sprintId = _fixture.Create<Guid>();
            var sprint = _fixture.Build<Sprint>()
                .With(x => x.Id, sprintId)
                .Create();

            _sprintRepository.GetById(sprintId).Returns(sprint);

            // when
            var result = await _subject.GetById(sprintId);

            // then
            await _sprintRepository.Received(1).GetById(sprintId);
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(sprint.Id));
            Assert.That(result.Name, Is.EqualTo(sprint.Name));
            Assert.That(result.StartDate, Is.EqualTo(sprint.StartDate));
            Assert.That(result.EndDate, Is.EqualTo(sprint.EndDate));
            Assert.That(result.Status, Is.EqualTo(sprint.Status));
        }

        [Test]
        public void Create()
        {
            // given
            var sprint = _fixture.Create<SprintViewModel>();

            // when
            _subject.Create(sprint);

            // then
            _sprintRepository.Received(1).Create(Arg.Is<Sprint>(x => x.Id == sprint.Id));
        }

        [Test]
        public void Update()
        {
            // given
            var sprint = _fixture.Create<SprintViewModel>();

            // when
            _subject.Update(sprint);

            // then
            _sprintRepository.Received(1).Update(Arg.Is<Sprint>(x => x.Id == sprint.Id));
        }

        [Test]
        public void Delete()
        {
            // given
            var sprint = _fixture.Create<SprintViewModel>();

            // when
            _subject.Delete(sprint);

            // then
            _sprintRepository.Received(1).Delete(Arg.Is<Sprint>(x => x.Id == sprint.Id));
        }

        [Test]
        public async Task GetSprintPointTotal()
        {
            // given
            var sprintId = _fixture.Create<Guid>();
            var story1Points = _fixture.Create<int>();
            var story1 = _fixture.Build<StoryViewModel>()
                .With(x => x.SprintId, sprintId)
                .With(x => x.Points, story1Points)
                .Create();
            var story2Points = _fixture.Create<int>();
            var story2 = _fixture.Build<StoryViewModel>()
                .With(x => x.SprintId, sprintId)
                .With(x => x.Points, story2Points)
                .Create();
            var stories = new List<StoryViewModel>
            {
                story1,
                story2
            };
            var sprint = _fixture.Build<Sprint>()
                .With(x => x.Id, sprintId)
                .Create();

            _sprintRepository.GetById(sprintId).Returns(sprint);
            _storyService.GetBySprint(sprintId).Returns(stories);

            // when
            var result = await _subject.GetById(sprintId);

            // then
            var expectedTotal = story1Points + story2Points;
            Assert.That(result.SprintPointTotal, Is.EqualTo(expectedTotal));
        }

        [Test]
        public async Task GetSprintPointTotal_ByStoryStatus()
        {
            // given
            var sprintId = _fixture.Create<Guid>();
            var story1Points = _fixture.Create<int>();
            var story1 = _fixture.Build<StoryViewModel>()
                .With(x => x.SprintId, sprintId)
                .With(x => x.Points, story1Points)
                .With(x => x.Status, StoryStatus.ToDo)
                .Create();
            var story2Points = _fixture.Create<int>();
            var story2 = _fixture.Build<StoryViewModel>()
                .With(x => x.SprintId, sprintId)
                .With(x => x.Points, story2Points)
                .With(x => x.Status, StoryStatus.ToDo)
                .Create();
            var story3Points = _fixture.Create<int>();
            var story3 = _fixture.Build<StoryViewModel>()
                .With(x => x.SprintId, sprintId)
                .With(x => x.Points, story3Points)
                .With(x => x.Status, StoryStatus.Done)
                .Create();
            var story4Points = _fixture.Create<int>();
            var story4 = _fixture.Build<StoryViewModel>()
                .With(x => x.SprintId, sprintId)
                .With(x => x.Points, story4Points)
                .With(x => x.Status, StoryStatus.Done)
                .Create();
            var stories = new List<StoryViewModel>
            {
                story1,
                story2,
                story3,
                story4
            };
            var sprint = _fixture.Build<Sprint>()
                .With(x => x.Id, sprintId)
                .Create();

            _sprintRepository.GetById(sprintId).Returns(sprint);
            _storyService.GetBySprint(sprintId).Returns(stories);
            _storyService.GetByStatus(StoryStatus.ToDo).Returns(new List<StoryViewModel> {story1, story2});
            _storyService.GetByStatus(StoryStatus.Done).Returns(new List<StoryViewModel> {story3, story4});

            // when
            var result = await _subject.GetById(sprintId);

            // then
            Assert.That(result.TotalPointsToDo, Is.EqualTo(story1Points + story2Points));
            Assert.That(result.TotalPointsDone, Is.EqualTo(story3Points + story4Points));
        }
    }
}