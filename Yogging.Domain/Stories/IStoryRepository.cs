using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Yogging.Domain.Stories
{
    public interface IStoryRepository
    {
        IEnumerable<Story> GetAll();
        Story GetById(Guid id);
        IEnumerable<Story> GetBySprintId(Guid id);
        void Create(Story story);
        Task Update(Story story);
        void Delete(Story story);
    }
}