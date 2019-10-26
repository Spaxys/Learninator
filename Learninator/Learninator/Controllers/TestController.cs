using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Learninator.Models;
using Learninator.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Learninator.Controllers
{
    public class TestController : Controller
    {
        private readonly ILogger _logger;
        public TestController(ILogger<TestController> logger)
        {
            _logger = logger;
        }
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
            var model = new TagsVM
            {
                Tags = new List<TaggingVM>()
            };
            ViewBag.LinkId = 1;
            return View(model);
        }

        [HttpPost]
        public IActionResult SearchTag(TagsVM model)
        {
            return View();
        }

        [HttpPost]
        public IActionResult TestPost([FromBody]TagsVM json)
        {
            _logger.LogInformation(JsonConvert.SerializeObject(json));
            return Json(new { Result = "Yay, it works!" });
        }
    }
}