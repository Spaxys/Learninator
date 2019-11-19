using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Learninator.Database;
using Learninator.Models;
using Learninator.ViewModels;
using Learninator.Repositories;
using Learninator.Services;
using Microsoft.AspNetCore.Authorization;

namespace Learninator.Controllers
{
    public class LinksController : Controller
    {
        private readonly LearninatorContext _context;
        private ITagsRepository _tagsRepository;
        private Learninator.Services.ILinksService _linksService;

        public LinksController(LearninatorContext context,
            ITagsRepository tagsRepository,
            ILinksService linksService)
        {
            _context = context;
            _tagsRepository = tagsRepository;
            _linksService = linksService;
        }

        // GET: Links
        public async Task<IActionResult> Index()
        {
            var linksWithTags = await _context.Links
                .Include(x => x.LinkTags)
                .ThenInclude(y => y.Tag)
                .AsNoTracking()
                .ToListAsync();
            return View(linksWithTags);
        }

        // GET: Links/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var link = await _context.Links
                .FirstOrDefaultAsync(m => m.Id == id);
            if (link == null)
            {
                return NotFound();
            }

            return View(link);
        }

        // GET: Links/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Links/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Url,Title")] Link link)
        {
            if (ModelState.IsValid)
            {
                _context.Add(link);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(link);
        }

        public async Task<IActionResult> CreateAjax(bool createMany)
        {
            var model = new LinkWithTagsVM
            {
                Link = new Link
                {

                },
                Tags = new List<Tag>()
            };
            ViewBag.CreateMany = createMany;
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAjax([FromBody]LinkWithTagsVM model)
        {

            var id = _linksService.CreateLinkWithTags(model);
            return Json(id);
        }

        // GET: Links/Edit/5
         public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var link = await _context.Links.FindAsync(id);
            if (link == null)
            {
                return NotFound();
            }
            return View(link);
        }

        // POST: Links/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Url,Title")] Link link)
        {
            if (id != link.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(link);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LinkExists(link.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(link);
        }

        // GET: Links/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var link = await _context.Links
                .FirstOrDefaultAsync(m => m.Id == id);
            if (link == null)
            {
                return NotFound();
            }

            return View(link);
        }

        // POST: Links/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var link = await _context.Links.FindAsync(id);
            _context.Links.Remove(link);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> GetTags(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var link = await _context.Links
                .FirstOrDefaultAsync(m => m.Id == id);
            if (link == null)
            {
                return NotFound();
            }
            var tags = _context.LinkTags
                .Where(m => m.LinkId == id)
                .Include(lt => lt.Tag)
                .Select(lt => lt.Tag)
                .ToList();


            ViewBag.LinkId = id;
            return View(tags);

        }

        public async Task<IActionResult> ConnectTags(int? id)
        {
            var tags = await _context.Tags
                .Select(n => new SelectListItem
                {
                    Value = n.Id.ToString(),
                    Text = n.Name
                }).ToListAsync();
            var model = new ConnectTagsVM
            {
                LinkId = id,
                TagId = null,
                Tags = tags
            };
            return View(model);
        }

        [HttpPost, ActionName("ConnectTags")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ConnectTags([Bind("LinkId, TagId")] ConnectTagsVM model)
        {
            var linkTag = new LinkTag
            {
                LinkId = (int)model.LinkId,
                TagId = (int)model.TagId
            };
            _context.Add(linkTag);
            await _context.SaveChangesAsync();

            return RedirectToAction("GetTags", new { id = model.LinkId });
        }

        public async Task<IActionResult> EditTags(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var linkWithTags = await _tagsRepository.GetLinkWithTags((int)id);
            var model = new TagsVM
            {
                LinkId = (int)id,
                Tags = linkWithTags.LinkTags.Select(x => new TaggingVM
                {
                    Id = (int?)x.Tag.Id,
                    Name = x.Tag.Name

                }).ToList()
            };
            //ViewBag.LinkId = linkId;
            return View(model);
        }

        private bool LinkExists(int id)
        {
            return _context.Links.Any(e => e.Id == id);
        }
    }
}
