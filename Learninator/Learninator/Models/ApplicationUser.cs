﻿using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Learninator.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Nickname { get; set; }
    }
}