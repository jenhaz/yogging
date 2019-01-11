using System.Collections.Generic;
using System.Linq;
using Yogging.DAL.Context;
using Yogging.DAL.Repository;
using Yogging.Domain.Users;
using Yogging.Models;

namespace Yogging.DAL.Users
{
    public class UserRepository : IUserRepository
    {
        private readonly YoggingContext _db;

        public UserRepository(YoggingContext db)
        {
            _db = db;
        }

        public IEnumerable<User> GetUsers()
        {
            var query = _db.Users.OrderBy(x => x.FirstName);

            return query.ToList();
        }
    }
}