using Learninator.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learninator.ViewModels
{
    public class ConnectTagsVM
    {
        public int? LinkId { get; set; }
        public int? TagId { get; set; }
        public List<SelectListItem> Tags { get; set; }
    }
}
