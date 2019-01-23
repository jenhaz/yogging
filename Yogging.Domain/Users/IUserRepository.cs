using System.Collections.Generic;

namespace Yogging.Domain.Users
{
    public interface IUserRepository
    {
        IEnumerable<User> GetUsers();
    }
}