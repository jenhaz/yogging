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
using Yogging.Services.Interfaces;

namespace Yogging.Tests.Services
{
    public class SprintServiceTests
    {
        private ISprintRepository _sprintRepository;
        private IStoryService _storyService;
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _sprintRepository = Substitute.For<ISprintRepository>();
            _storyService = Substitute.For<IStoryService>();
            _fixture = new Fixture();
        }

        [Test]
        public void GetAllActiveSprints()
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

            _sprintRepository.GetSprints().Returns(sprints);

            // when
            var result = new SprintService(_sprintRepository, _storyService).GetAllActiveSprints().ToList();

            // then
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
        public void GetAllClosedSprints()
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

            _sprintRepository.GetSprints().Returns(sprints);

            // when
            var result = new SprintService(_sprintRepository, _storyService).GetAllClosedSprints().ToList();

            // then
            Assert.That(result.Count, Is.EqualTo(1));
            var actual = result.FirstOrDefault(x => x.Id == closedSprintId);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.EqualTo(closedSprint.Id));
            Assert.That(actual.Name, Is.EqualTo(closedSprint.Name));
            Assert.That(actual.StartDate, Is.EqualTo(closedSprint.StartDate));
            Assert.That(actual.EndDate, Is.EqualTo(closedSprint.EndDate));
            Assert.That(actual.Status, Is.EqualTo(closedSprint.Status));
        }
    }
}