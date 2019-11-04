using Learninator.Models;
using Learninator.ViewModels;

namespace Learninator.Services
{
    public interface ILinksService
    {
        int CreateLinkWithTags(LinkWithTagsVM link);
    }
}