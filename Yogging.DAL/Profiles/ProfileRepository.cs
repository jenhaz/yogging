using System;
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

        public IEnumerable<Profile> GetAll()
        {
            var query = _db.Profiles.Select(x => MapTo(x)).OrderBy(x => x.Id);

            return query.ToList();
        }

        public Profile GetById(Guid id)
        {
            var dao = _db.Profiles.FirstOrDefault(x => x.Id == id);

            return MapTo(dao);
        }

        public void Create(Profile profile)
        {
            var dao = MapTo(profile);
            _db.Profiles.Add(dao);
            _db.SaveChanges();
        }

        public void Update(Profile profile)
        {
            var dao = MapTo(profile);
            _db.Entry(profile).State = EntityState.Modified;
            _db.SaveChanges();
        }

        private ProfileDao MapTo(Profile profile)
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

        private Profile MapTo(ProfileDao dao)
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