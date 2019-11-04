using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Learninator.Database;
using Learninator.Models;
using Microsoft.Extensions.Logging;

namespace Learninator.Repositories
{
    public class LinksRepository : ILinksRepository
    {
        private readonly LearninatorContext _context;
        private readonly ILogger<LinksRepository> _logger;
        public LinksRepository(LearninatorContext context,
            ILogger<LinksRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public int CreateLink(Link link)
        {
            _context.Add(link);
            _context.SaveChanges();
            return link.Id;
        }
    }
}
