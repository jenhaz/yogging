using System.Collections.Generic;
using Yogging.Models;

namespace Yogging.Domain.Tags
{
    public interface ITagRepository
    {
        IEnumerable<Tag> GetTags();
        Tag GetTagById(int? id);
    }
}