﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<IEnumerable<Profile>> GetAll()
        {
            var profiles = await _db.Profiles.ToListAsync();
            
            if (profiles.Any())
            {
                return profiles.Select(MapTo).OrderBy(x => x.Id);
            }

            return null;
        }

        public async Task<Profile> GetById(Guid id)
        {
            var dao = await _db.Profiles.FirstOrDefaultAsync(x => x.Id == id);

            return MapTo(dao);
        }

        public async Task Create(Profile profile)
        {
            var dao = MapTo(profile);
            _db.Profiles.Add(dao);
            await _db.SaveChangesAsync();
        }

        public async Task Update(Profile profile)
        {
            var dao = MapTo(profile);
            _db.Entry(dao).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        private static ProfileDao MapTo(Profile profile)
        {
            return new ProfileDao
            {
                BlogUrl = profile.BlogUrl,
                Blurb = profile.Blurb,
                ContactEmailAddress = profile.ContactEmailAddress,
                CurrentJobTitle = profile.CurrentJobTitle,
                FullName = profile.FullName,
                GitHubUsername = profile.GitHubUsername,
                Id = profile.Id,
                ImageUrl = profile.ImageUrl,
                InstagramUsername = profile.InstagramUsername,
                LinkedInUsername = profile.LinkedInUsername,
                LongerBlurb = profile.LongerBlurb,
                TwitterUsername = profile.TwitterUsername
            };
        }

        private static Profile MapTo(ProfileDao dao)
        {
            return new Profile
            {
                BlogUrl = dao.BlogUrl,
                Blurb = dao.Blurb,
                ContactEmailAddress = dao.ContactEmailAddress,
                CurrentJobTitle = dao.CurrentJobTitle,
                FullName = dao.FullName,
                GitHubUsername = dao.GitHubUsername,
                Id = dao.Id,
                ImageUrl = dao.ImageUrl,
                InstagramUsername = dao.InstagramUsername,
                LinkedInUsername = dao.LinkedInUsername,
                LongerBlurb = dao.LongerBlurb,
                TwitterUsername = dao.TwitterUsername
            };
        }
    }
}