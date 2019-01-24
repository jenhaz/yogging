using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Yogging.DAL.Context;
using Yogging.Domain.Stories;

namespace Yogging.DAL.Stories
{
    public class StoryRepository : IStoryRepository
    {
        private readonly YoggingContext _db;

        public StoryRepository(YoggingContext db)
        {
            _db = db;
        }

        public IEnumerable<Story> GetAll()
        {
            var stories = _db.Stories.Select(x => MapTo(x)).OrderByDescending(x => x.Id);

            return stories.ToList();
        }

        public Story GetById(Guid id)
        {
            var story = _db.Stories.FirstOrDefault(x => x.Id == id);

            return MapTo(story);
        }

        public IEnumerable<Story> GetBySprintId(Guid id)
        {
            var stories = _db.Stories.Where(x => x.Sprint.Id == id).Select(x => MapTo(x));

            return stories;
        }

        public void Create(Story story)
        {
            var dao = MapTo(story);
            dao.CreatedDate = DateTime.UtcNow;
            dao.LastUpdated = DateTime.UtcNow;
            _db.Stories.Add(dao);
            _db.SaveChanges();
        }

        public async Task Update(Story story)
        {
            var dao = MapTo(story);
            _db.Stories.Attach(dao);
            dao.LastUpdated = DateTime.UtcNow;
            _db.Entry(dao).State = EntityState.Modified;
            _db.Entry(dao).Property("CreatedDate").IsModified = false;
            await _db.SaveChangesAsync();
        }

        public void Delete(Story story)
        {
            var dao = MapTo(story);
            _db.Stories.Remove(dao);
            _db.SaveChanges();
        }

        private static Story MapTo(StoryDao dao)
        {
            return new Story
            {
                AcceptanceCriteria = dao.AcceptanceCriteria,
                Id = dao.Id,
                Name = dao.Name,
                CreatedDate = dao.CreatedDate,
                LastUpdated = dao.LastUpdated,
                Priority = (Priority)Enum.Parse(typeof(Priority), dao.Priority),
                Type = (TaskType)Enum.Parse(typeof(TaskType), dao.Type),
                Description = dao.Description,
                Points = dao.Points,
                Status = (StoryStatus)Enum.Parse(typeof(StoryStatus), dao.Status),
                UserId = dao.User.Id,
                SprintId = dao.Sprint.Id,
                TagId = dao.Tag.Id
            };
        }

        private static StoryDao MapTo(Story story)
        {
            return new StoryDao
            {
                AcceptanceCriteria = story.AcceptanceCriteria,
                Id = story.Id,
                Name = story.Name,
                CreatedDate = story.CreatedDate,
                LastUpdated = story.LastUpdated,
                Priority = story.Priority.ToString(),
                Type = story.Type.ToString(),
                Description = story.Description,
                Points = story.Points,
                Status = story.Status.ToString(),
                UserId = story.UserId,
                SprintId = story.SprintId,
                TagId = story.TagId
            };
        }
    }
}