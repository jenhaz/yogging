using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Yogging.Domain.Sprints
{
    public interface ISprintRepository
    {
        Task<IEnumerable<Sprint>> GetAll();
        Task<Sprint> GetById(Guid id);
        void Create(Sprint sprint);
        void Update(Sprint sprint);
        void Delete(Sprint sprint);
    }
}