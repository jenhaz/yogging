using System;
using System.Collections.Generic;

namespace Yogging.Domain.Tags
{
    public interface ITagRepository
    {
        IEnumerable<Tag> GetAll();
        Tag GetById(Guid id);
        void Create(Tag tag);
        void Update(Tag tag);
        void Delete(Tag tag);
    }
}