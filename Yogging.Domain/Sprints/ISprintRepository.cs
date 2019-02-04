using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Yogging.Domain.Sprints
{
    public interface ISprintRepository
    {
        Task<IEnumerable<Sprint>> GetAll();
        Task<Sprint> GetById(Guid id);
        Task Create(Sprint sprint);
        Task Update(Sprint sprint);
        Task Delete(Sprint sprint);
    }
}