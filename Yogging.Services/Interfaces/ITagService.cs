using System;
using System.Collections.Generic;
using Yogging.Models.ViewModels;

namespace Yogging.Services.Interfaces
{
    public interface ITagService
    {
        IEnumerable<TagViewModel> GetAllTags();
        TagViewModel GetTagById(Guid? id);
    }
}
