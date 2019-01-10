using System.Collections.Generic;
using Yogging.Models;

namespace Yogging.DAL.Repository
{
    public interface ITagRepository
    {
        IEnumerable<Tag> GetTags();
        Tag GetTagById(int? id);
    }
}