using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Yogging.Domain.Stories
{
    public interface IStoryRepository
    {
        Task<IEnumerable<Story>> GetAll();
        Task<Story> GetById(Guid id);
        Task<IEnumerable<Story>> GetBySprintId(Guid id);
        Task Create(Story story);
        Task Update(Story story);
        Task Delete(Story story);
    }
}