using System.Collections.Generic;
using System.Threading.Tasks;
using Learninator.Models;

namespace Learninator.Repositories
{
    public interface ITagsRepository
    {
        Task<bool> SaveTagsOnLink(List<Tag> tags, int linkId);
    }
}