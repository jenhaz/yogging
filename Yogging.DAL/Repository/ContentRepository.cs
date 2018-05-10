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
            IOrderedQueryable<Story> query = db.Stories.OrderByDescending(x => x.Id);

            return query.ToList();
        }

        public IEnumerable<Sprint> GetSprints()
        {
            IOrderedQueryable<Sprint> query = db.Sprints.OrderByDescending(x => x.Id);

            return query.ToList();
        }

        public IEnumerable<Tag> GetTags()
        {
            IOrderedQueryable<Tag> query = db.Tags.OrderBy(x => x.Name);

            return query.ToList();
        }

        public Tag GetTagById(int? id)
        {
            Tag query = db.Tags.Where(x => x.Id.Equals(id)).FirstOrDefault();

            return query;
        }

        public IEnumerable<User> GetUsers()
        {
            IOrderedQueryable<User> query = db.Users.OrderBy(x => x.FirstName);

            return query.ToList();
        }

        public IEnumerable<Profile> GetProfiles()
        {
            IOrderedQueryable<Profile> query = db.Profiles.OrderBy(x => x.Id);

            return query.ToList();
        }
    }
}