using AutoFixture;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using Yogging.Domain.Tags;
using Yogging.Stories;
using Yogging.Tags;

namespace Yogging.Tests.Services
{
    public class TagServiceTests
    {
        private ITagRepository _tagRepository;
        private IStoryService _storyService;
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _tagRepository = Substitute.For<ITagRepository>();
            _storyService = Substitute.For<IStoryService>();
            _fixture = new Fixture();
        }

        [Test]
        public void GetAllTags()
        {
            // given
            var tagId = Guid.NewGuid();
            var tag = _fixture
                .Build<Tag>()
                .With(x => x.Id, tagId)
                .Create();
            var tags = new List<Tag>
            {
                tag
            };
            _tagRepository.GetTags().Returns(tags);

            // when
            var result = new TagService(_tagRepository, _storyService).GetAllTags().ToList();

            // then
            Assert.That(result.Count, Is.EqualTo(1));
            var actual = result.FirstOrDefault(x => x.Id == tagId);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.EqualTo(tag.Id));
            Assert.That(actual.Name, Is.EqualTo(tag.Name));
            Assert.That(actual.Colour, Is.EqualTo(tag.Colour));
        }

        [Test]
        public void GetTagById()
        {
            // given
            var tagId = Guid.NewGuid();
            var tag = _fixture
                .Build<Tag>()
                .With(x => x.Id, tagId)
                .Create();
            var tags = new List<Tag>
            {
                tag
            };
            _tagRepository.GetTags().Returns(tags);

            // when
            var result = new TagService(_tagRepository, _storyService).GetTagById(tagId);

            // then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(tag.Id));
            Assert.That(result.Name, Is.EqualTo(tag.Name));
            Assert.That(result.Colour, Is.EqualTo(tag.Colour));
        }
    }
}