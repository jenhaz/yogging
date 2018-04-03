using System.Collections.Generic;
using Yogging.Models;

namespace Yogging.DAL.Repository
{
    public interface IContentRepository
    {
        IEnumerable<Story> GetStories();
        IEnumerable<Sprint> GetSprints();
        IEnumerable<Tag> GetTags();
        Tag GetTagById(int? id);
        IEnumerable<User> GetUsers();
    }
}
