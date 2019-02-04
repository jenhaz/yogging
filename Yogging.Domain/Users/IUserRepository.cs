using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Yogging.Domain.Users
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAll();
        Task<User> GetById(Guid id);
        Task Create(User user);
        Task Update(User user);
        Task Delete(User user);
    }
}