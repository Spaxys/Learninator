using Learninator.Database;
using Learninator.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Learninator.Library.Extensions;

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
            tags = tags.DistinctBy(x => x.Id).ToList();
            //Update behavior inspired by the following post:
            // https://stackoverflow.com/questions/42735368/updating-related-data-with-entity-framework-core
            var link = _context.Links
                .Include(x => x.LinkTags)
                .ThenInclude(y => y.Tag)
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == linkId);

            if(link == null)
                throw new Exception("No link on this id");

            var oldLinkTags = link.LinkTags;
            // Remove unused tags
            foreach(var olt in oldLinkTags)
            {
                var linkTag = tags.SingleOrDefault(x => x.Id == olt.TagId);
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
            _context.SaveChanges();
            return true;
        }

        public async Task<Link> GetLinkWithTags(int linkId)
        {
            var linkWithTags = await _context.Links
                .Include(x => x.LinkTags)
                .ThenInclude(y => y.Tag)
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == linkId);
            return linkWithTags;
        }

        public async Task<Tag> CreateOrGetTagByName(string name)
        {
            var existingTag = _context.Tags.
                FirstOrDefault(t => name.Equals(t.Name, StringComparison.OrdinalIgnoreCase));
            if (existingTag != null)
                return existingTag;
            else
            {
                var tag = new Tag
                {
                    Name = name
                };
                _context.Add(tag);
                await _context.SaveChangesAsync();
                return tag;
            }
        }
    }
}
