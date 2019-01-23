using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Tag> GetTags()
        {
            var query = _db.Tags.OrderBy(x => x.Name);

            return query.ToList();
        }

        public Tag GetTagById(int? id)
        {
            var query = _db.Tags.FirstOrDefault(x => x.Id.Equals(id));

            return query;
        }
    }
}