using Learninator.Database;
using Learninator.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learninator.Repositories
{
    public class TagsRepository : ITagsRepository
    {
        private readonly LearninatorContext _context;
        private readonly ILogger<TagsRepository> _logger;

        public TagsRepository(LearninatorContext context,
            ILogger<TagsRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<bool> SaveTagsOnLink(List<Tag> tags, int linkId)
        {
            var link = _context.Links
                .Include(x => x.LinkTags)
                .ThenInclude(y => y.Tag)
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == linkId);

            if(link == null)
                throw new Exception("No link on this id");

            var oldLinkTags = link.LinkTags;
            foreach(var olt in oldLinkTags)
            {
                var linkTag = tags.SingleOrDefault(x => x.Id == olt.LinkId);
                if (linkTag == null)
                    _context.Remove(olt);
                _logger.LogInformation("Removed LinkTag with TagId: " + olt.TagId);
            }
            var linkTags = tags
                .Select(x =>
                {
                    return new LinkTag
                    {
                        LinkId = linkId,
                        TagId = x.Id
                    };
                });
            // add the new items
            foreach (var lt in linkTags)
            {
                if (oldLinkTags.All(i => i.TagId != lt.TagId))
                {
                    _context.Add(lt);
                    _logger.LogInformation("Added LinkTag with TagId: " + lt.TagId);

                }
            }
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
