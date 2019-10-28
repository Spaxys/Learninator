using System.Collections.Generic;
using System.Threading.Tasks;
using Learninator.Models;

namespace Learninator.Repositories
{
    public interface ITagsRepository
    {
        Task<Tag> CreateOrGetTagByName(string name);
        Task<Link> GetLinkWithTags(int linkId);
        Task<bool> SaveTagsOnLink(List<Tag> tags, int linkId);
    }
}