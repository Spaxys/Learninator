using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Learninator.Models;
using Learninator.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace Learninator.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Tagging()
        {
            return View();
        }

        public IActionResult SearchTag()
        {
            return View();
        }
    }
}