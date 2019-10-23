using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learninator.ViewModels
{
    public class TagsVM
    {
        public int LinkId { get; set; }
        public List<TaggingVM> Tags { get; set; }
    }
}
