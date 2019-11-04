using Learninator.Models;

namespace Learninator.Repositories
{
    public interface ILinksRepository
    {
        int CreateLink(Link link);
    }
}