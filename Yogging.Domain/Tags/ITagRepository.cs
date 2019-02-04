using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Yogging.Domain.Tags
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAll();
        Task<Tag> GetById(Guid id);
        Task Create(Tag tag);
        Task Update(Tag tag);
        Task Delete(Tag tag);
    }
}