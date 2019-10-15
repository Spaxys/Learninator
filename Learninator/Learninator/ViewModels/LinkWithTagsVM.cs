using Learninator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learninator.ViewModels
{
    public class LinkWithTagsVM
    {
        public Link Link { get; set; }
        public List<Tag> Tags { get; set; }
    }
}
