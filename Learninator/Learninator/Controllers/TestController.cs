using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Learninator.Models;
using Learninator.Repositories;
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
        private readonly ITagsRepository _tagsRepository;

        public TestController(ILogger<TestController> logger, ITagsRepository tagsRepository)
        {
            _logger = logger;
            _tagsRepository = tagsRepository;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Tagging()
        {
            return View();
        }

        public async Task<IActionResult> SearchTag()
        {
            var linkId = 1;
            var linkWithTags = await _tagsRepository.GetLinkWithTags(linkId);
            var model = new TagsVM
            {
                LinkId = linkId,
                Tags = linkWithTags.LinkTags.Select(x => new TaggingVM
                {
                    Id = (int?)x.Tag.Id,
                    Name = x.Tag.Name

                }).ToList()
            };
            ViewBag.LinkId = linkId;
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