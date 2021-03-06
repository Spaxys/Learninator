﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learninator.Models
{
    public class Link
    {
        public int Id { get; set; }
        public string Url { get; set; }
        public string Title { get; set; }
        public ICollection<LinkTag> LinkTags { get; set; }
    }
}
