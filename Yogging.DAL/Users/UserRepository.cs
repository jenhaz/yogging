using System.Collections.Generic;
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

        public IEnumerable<User> GetUsers()
        {
            var query = _db.Users.OrderBy(x => x.FirstName);

            return query.ToList();
        }
    }
}