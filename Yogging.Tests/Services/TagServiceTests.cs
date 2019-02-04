using AutoFixture;
using NSubstitute;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Yogging.Domain.Tags;
using Yogging.Tags;
using Yogging.ViewModels;

namespace Yogging.Tests.Services
{
    public class TagServiceTests
    {
        private ITagRepository _tagRepository;
        private TagService _subject;
        private Fixture _fixture;

        [SetUp]
        public void SetUp()
        {
            _tagRepository = Substitute.For<ITagRepository>();
            _subject = new TagService(_tagRepository);
            _fixture = new Fixture();
        }

        [Test]
        public async Task GetAllTags()
        {
            // given
            var tagId = Guid.NewGuid();
            var tag = _fixture.Build<Tag>()
                .With(x => x.Id, tagId)
                .Create();

            var tags = new List<Tag>
            {
                tag
            };

            _tagRepository.GetAll().Returns(tags);

            // when
            var result = await _subject.GetAll();

            // then
            Assert.That(result.Count, Is.EqualTo(1));
            var actual = result.FirstOrDefault(x => x.Id == tagId);
            Assert.That(actual, Is.Not.Null);
            Assert.That(actual.Id, Is.EqualTo(tag.Id));
            Assert.That(actual.Name, Is.EqualTo(tag.Name));
            Assert.That(actual.Colour, Is.EqualTo(tag.Colour));
        }

        [Test]
        public async Task GetTagById()
        {
            // given
            var tagId = Guid.NewGuid();
            var tag = _fixture.Build<Tag>()
                .With(x => x.Id, tagId)
                .Create();

            _tagRepository.GetById(tagId).Returns(tag);

            // when
            var result = await _subject.GetById(tagId);

            // then
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(tag.Id));
            Assert.That(result.Name, Is.EqualTo(tag.Name));
            Assert.That(result.Colour, Is.EqualTo(tag.Colour));
        }

        [Test]
        public void Create()
        {
            // given
            var tag = _fixture.Create<TagViewModel>();

            // when
            _subject.Create(tag);

            // then
            _tagRepository.Received(1).Create(Arg.Is<Tag>(x => x.Id == tag.Id));
        }

        [Test]
        public void Update()
        {
            // given
            var tag = _fixture.Create<TagViewModel>();

            // when
            _subject.Update(tag);

            // then
            _tagRepository.Received(1).Update(Arg.Is<Tag>(x => x.Id == tag.Id));
        }

        [Test]
        public void Delete()
        {
            // given
            var tag = _fixture.Create<TagViewModel>();

            // when
            _subject.Delete(tag);

            // then
            _tagRepository.Received(1).Delete(Arg.Is<Tag>(x => x.Id == tag.Id));
        }
    }
}