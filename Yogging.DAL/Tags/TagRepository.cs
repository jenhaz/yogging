using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<Tag>> GetAll()
        {
            var tags = await _db.Tags.ToListAsync();

            return tags.Select(MapTo).OrderBy(x => x.Name);
        }

        public async Task<Tag> GetById(Guid id)
        {
            var tag = await _db.Tags.FirstOrDefaultAsync(x => x.Id == id);

            return MapTo(tag);
        }

        public async Task Create(Tag tag)
        {
            var dao = MapTo(tag);
            _db.Tags.Add(dao);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Tag tag)
        {
            var dao = MapTo(tag);
            _db.Entry(dao).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Tag tag)
        {
            var dao = MapTo(tag);
            _db.Tags.Remove(dao);
            await _db.SaveChangesAsync();
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