using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Yogging.DAL.Context;
using Yogging.Domain.Users;

namespace Yogging.DAL.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly YoggingContext _db;

        public UserRepository(YoggingContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<User>> GetAll()
        {
            var users = await _db.Users.ToListAsync();
            return users.Select(MapTo).OrderBy(x => x.FirstName);
        }

        public async Task<User> GetById(Guid id)
        {
            var user = await _db.Users.FirstOrDefaultAsync(x => x.Id == id);
            return MapTo(user);
        }

        public async Task Create(User user)
        {
            var dao = MapTo(user);
            _db.Users.Add(dao);
            await _db.SaveChangesAsync();
        }

        public async Task Update(User user)
        {
            var dao = MapTo(user);
            _db.Entry(dao).State = EntityState.Modified;
            await _db.SaveChangesAsync();
        }

        public async Task Delete(User user)
        {
            var dao = MapTo(user);
            _db.Users.Remove(dao);
            await _db.SaveChangesAsync();
        }

        private static User MapTo(UserDao dao)
        {
            return new User
            {
                Id = dao.Id,
                EmailAddress = dao.EmailAddress,
                FirstName = dao.FirstName,
                LastName = dao.LastName,
                IsInactive = dao.IsInactive
            };
        }
        private static UserDao MapTo(User user)
        {
            return new UserDao
            {
                Id = user.Id,
                EmailAddress = user.EmailAddress,
                FirstName = user.FirstName,
                LastName = user.LastName,
                IsInactive = user.IsInactive
            };
        }
    }
}