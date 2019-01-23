﻿using System.Collections.Generic;
using System.Linq;
using Yogging.DAL.Context;
using Yogging.Domain.Profiles;

namespace Yogging.DAL.Profiles
{
    public class ProfileRepository : IProfileRepository
    {
        private readonly YoggingContext _db;

        public ProfileRepository(YoggingContext db)
        {
            _db = db;
        }

        public IEnumerable<Profile> GetProfiles()
        {
            var query = _db.Profiles.OrderBy(x => x.Id);

            return query.ToList();
        }
    }
}