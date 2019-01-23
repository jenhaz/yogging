using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        public IEnumerable<Sprint> GetAll()
        {
            var query = _db.Sprints.Select(x => MapTo(x)).OrderByDescending(x => x.Id);

            return query.ToList();
        }

        public Sprint GetById(Guid id)
        {
            var sprint = _db.Sprints.Find(id);
            return MapTo(sprint);
        }

        public void Create(Sprint sprint)
        {
            var dao = MapTo(sprint);
            _db.Sprints.Add(dao);
            _db.SaveChanges();
        }

        public void Update(Sprint sprint)
        {
            var dao = MapTo(sprint);
            _db.Entry(dao).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(Sprint sprint)
        {
            var dao = MapTo(sprint);
            _db.Sprints.Remove(dao);
            _db.SaveChanges();
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