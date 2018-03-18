using System.Collections.Generic;
using System.Linq;
using Yogging.DAL.Context;
using Yogging.DAL.Repository;
using Yogging.Models;
using Yogging.Models.ViewModels;
using Yogging.Services.Interfaces;

namespace Yogging.Services.Implementations
{
    public class TagService : ITagService
    {
        private YoggingContext db = new YoggingContext();
        private IContentRepository ContentRepository { get; }

        public TagService(IContentRepository contentRepository)
        {
            ContentRepository = contentRepository;
        }

        public IEnumerable<TagViewModel> GetAllTags()
        {
            IEnumerable<TagViewModel> tags = ContentRepository.GetTags()
                .Select(x => GetTag(x));

            return tags;
        }

        public TagViewModel GetTagById(int? id)
        {
            TagViewModel tag = ContentRepository.GetTags().Where(y => y.Id.Equals(id))
                .Select(x => GetTag(x)).FirstOrDefault();

            return tag;
        }

        private TagViewModel GetTag(Tag tag)
        {
            return new TagViewModel
            {
                Id = tag.Id,
                Name = tag.Name
            };
        }
    }
}
