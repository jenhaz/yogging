using System.Collections.Generic;

namespace Yogging.Domain.Tags
{
    public interface ITagRepository
    {
        IEnumerable<Tag> GetTags();
        Tag GetTagById(int? id);
    }
}