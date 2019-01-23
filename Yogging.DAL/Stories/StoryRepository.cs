﻿using System.Collections.Generic;
using System.Linq;
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

        public IEnumerable<Story> GetStories()
        {
            var query = _db.Stories.OrderByDescending(x => x.Id);

            return query.ToList();
        }
    }
}