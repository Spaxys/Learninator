using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learninator.Models
{
    public class LinkTag
    {
        public int LinkId { get; set; }
        public Link Link { get; set; }
        public int TagId { get; set; }
        public Tag Tag { get; set; }
    }
}
