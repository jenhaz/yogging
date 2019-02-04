using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Yogging.Domain.Users
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(Guid id);
        void Create(User user);
        void Update(User user);
        void Delete(User user);
    }
}