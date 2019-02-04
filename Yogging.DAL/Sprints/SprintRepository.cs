using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Yogging.DAL.Context;
using Yogging.Domain.Sprints;

namespace Yogging.DAL.Sprints
{
    public class SprintRepository : ISprintRepository
    {
        private readonly YoggingContext _db;

        public SprintRepository(YoggingContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Sprint>> GetAll()
        {
            var query = await _db.Sprints.ToListAsync();

            return query.Select(MapTo).OrderByDescending(x => x.Id);
        }

        public async Task<Sprint> GetById(Guid id)
        {
            var sprint = await _db.Sprints.FindAsync(id);
            return MapTo(sprint);
        }

        public async Task Create(Sprint sprint)
        {
            var dao = MapTo(sprint);
            _db.Sprints.Add(dao);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Sprint sprint)
        {
            var dao = MapTo(sprint);
            _db.Entry(dao).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task Delete(Sprint sprint)
        {
            var dao = MapTo(sprint);
            _db.Sprints.Remove(dao);
            await _db.SaveChangesAsync();
        }

        private static Sprint MapTo(SprintDao dao)
        {
            return new Sprint
            {
                Id = dao.Id,
                Name = dao.Name,
                StartDate = dao.StartDate,
                EndDate = dao.EndDate,
                Status = dao.Status,
                Stories = dao.Stories
            };
        }

        private static SprintDao MapTo(Sprint sprint)
        {
            return new SprintDao
            {
                Id = sprint.Id,
                Name = sprint.Name,
                StartDate = sprint.StartDate,
                EndDate = sprint.EndDate,
                Status = sprint.Status,
                Stories = sprint.Stories
            };
        }
    }
}