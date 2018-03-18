using System.Collections.Generic;
using System.Linq;
using Yogging.DAL.Context;
using Yogging.Models;

namespace Yogging.DAL.Repository
{
    public class ContentRepository : IContentRepository
    {
        private YoggingContext db { get; }

        public ContentRepository(YoggingContext context)
        {
            db = context;
        }

        public IEnumerable<Story> GetStories()
        {
            var query = db.Stories.OrderByDescending(x => x.CreatedDate);

            return query.ToList();
        }

        public IEnumerable<Sprint> GetSprints()
        {
            var query = db.Sprints.OrderByDescending(x => x.Id);

            return query.ToList();
        }

        public IEnumerable<Tag> GetTags()
        {
            var query = db.Tags.OrderBy(x => x.Name);

            return query.ToList();
        }

        public Tag GetTagById(int? id)
        {
            var query = db.Tags.Where(x => x.Id.Equals(id)).FirstOrDefault();

            return query;
        }
    }
}
