using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using Yogging.DAL.Context;
using Yogging.Domain.Tags;

namespace Yogging.DAL.Tags
{
    public class TagRepository : ITagRepository
    {
        private readonly YoggingContext _db;

        public TagRepository(YoggingContext db)
        {
            _db = db;
        }

        public IEnumerable<Tag> GetAll()
        {
            var tags = _db.Tags.Select(x => MapTo(x)).OrderBy(x => x.Name);

            return tags.ToList();
        }

        public Tag GetById(Guid id)
        {
            var tag = _db.Tags.FirstOrDefault(x => x.Id == id);

            return MapTo(tag);
        }

        public void Create(Tag tag)
        {
            var dao = MapTo(tag);
            _db.Tags.Add(dao);
            _db.SaveChanges();
        }

        public void Update(Tag tag)
        {
            var dao = MapTo(tag);
            _db.Entry(dao).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(Tag tag)
        {
            var dao = MapTo(tag);
            _db.Tags.Remove(dao);
            _db.SaveChanges();
        }

        private static Tag MapTo(TagDao dao)
        {
            return new Tag
            {
                Id = dao.Id,
                Colour = dao.Colour,
                Name = dao.Name
            };
        }

        private static TagDao MapTo(Tag tag)
        {
            return new TagDao
            {
                Id = tag.Id,
                Colour = tag.Colour,
                Name = tag.Name
            };
        }
    }
}