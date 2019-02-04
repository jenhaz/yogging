using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Yogging.Domain.Tags
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> GetAll();
        Task<Tag> GetById(Guid id);
        void Create(Tag tag);
        void Update(Tag tag);
        void Delete(Tag tag);
    }
}