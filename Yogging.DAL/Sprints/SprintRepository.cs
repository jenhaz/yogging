using System.Collections.Generic;
using System.Linq;
using Yogging.DAL.Context;
using Yogging.Domain.Sprints;
using Yogging.Models;

namespace Yogging.DAL.Repository
{
    public class SprintRepository : ISprintRepository
    {
        private readonly YoggingContext _db;

        public SprintRepository(YoggingContext db)
        {
            _db = db;
        }

        public IEnumerable<Sprint> GetSprints()
        {
            var query = _db.Sprints.OrderByDescending(x => x.Id);

            return query.ToList();
        }
    }
}