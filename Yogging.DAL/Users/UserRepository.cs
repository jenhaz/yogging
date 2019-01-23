using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        public IEnumerable<User> GetAll()
        {
            var users = _db.Users.Select(x => MapTo(x)).OrderBy(x => x.FirstName);

            return users.ToList();
        }

        public User GetById(Guid id)
        {
            var user = _db.Users.FirstOrDefault(x => x.Id == id);

            return MapTo(user);
        }

        public void Create(User user)
        {
            var dao = MapTo(user);
            _db.Users.Add(dao);
            _db.SaveChanges();
        }

        public void Update(User user)
        {
            var dao = MapTo(user);
            _db.Entry(dao).State = EntityState.Modified;
            _db.SaveChanges();
        }

        public void Delete(User user)
        {
            var dao = MapTo(user);
            _db.Users.Remove(dao);
            _db.SaveChanges();
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