using System;
using System.Collections.Generic;

namespace Yogging.Domain.Users
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User GetById(Guid id);
        void Create(User user);
        void Update(User user);
        void Delete(User user);
    }
}