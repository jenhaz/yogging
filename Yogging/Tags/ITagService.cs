using System;
using System.Collections.Generic;
using Yogging.ViewModels;

namespace Yogging.Tags
{
    public interface ITagService
    {
        IEnumerable<TagViewModel> GetAllTags();
        TagViewModel GetTagById(Guid? id);
    }
}
