using Learninator.Models;
using Learninator.Repositories;
using Learninator.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learninator.Services
{
    
    public class LinksService : ILinksService
    {
        private readonly ITagsRepository tagsRepository;
        private readonly Learninator.Repositories.ILinksRepository linksRepository;

        public LinksService(ITagsRepository tagsRepository,
            ILinksRepository linksRepository)
        {
            this.tagsRepository = tagsRepository;
            this.linksRepository = linksRepository;
        }

        public int CreateLinkWithTags(LinkWithTagsVM link)
        {
            var linkId = linksRepository.CreateLink(link.Link);
            tagsRepository.SaveTagsOnLink(link.Tags, linkId);
            return linkId;
        }
    }
}
