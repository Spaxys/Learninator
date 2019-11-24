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
using Newtonsoft.Json;
using Microsoft.AspNetCore.Authorization;

namespace Learninator.Controllers
{
    [Authorize(Roles = "Admin")]
    public class TagsController : Controller
    {
        private readonly LearninatorContext _context;
        private readonly ITagsRepository _tagsRepository;

        public TagsController(LearninatorContext context, 
            ITagsRepository tagsRepository)
        {
            _context = context;
            _tagsRepository = tagsRepository;
        }

        // GET: Tags
        public async Task<IActionResult> Index()
        {
            return View(await _context.Tags.ToListAsync());
        }

        // GET: Tags/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _context.Tags
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        // GET: Tags/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Tags/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Tag tag)
        {
            if (ModelState.IsValid)
            {
                _context.Add(tag);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tag);
        }

        // GET: Tags/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _context.Tags.FindAsync(id);
            if (tag == null)
            {
                return NotFound();
            }
            return View(tag);
        }

        // POST: Tags/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Tag tag)
        {
            if (id != tag.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(tag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TagExists(tag.Id))
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
            return View(tag);
        }

        // GET: Tags/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var tag = await _context.Tags
                .FirstOrDefaultAsync(m => m.Id == id);
            if (tag == null)
            {
                return NotFound();
            }

            return View(tag);
        }

        // POST: Tags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var tag = await _context.Tags.FindAsync(id);
            _context.Tags.Remove(tag);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> GetTagByName(string name)
        {
            if (name == null)
            {
                return NotFound();
            }

            var tag = await _context.Tags.FirstOrDefaultAsync(x => x.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
            if (tag == null)
            {
                return NotFound();
            }
            var result = new
            {
                Id = tag.Id,
                Name = tag.Name
            };
            return Json(result);
        }
        public async Task<IActionResult> SearchTagByName(string name)
        {
            if (name == null)
            {
                return NotFound();
            }

            var tags = await _context.Tags.Where(x => x.Name.Contains(name, StringComparison.OrdinalIgnoreCase)).ToListAsync();
            if (!tags.Any())
            {
                return NotFound();
            }
            return Json(tags);
        }

        [HttpPost]
        public async Task<IActionResult> SaveTagsOnLink([FromBody]TagsVM model)
        {
            if(model == null)
            {
                return BadRequest();
            }
            var link = _context.Links.Find(model.LinkId);
            if(link == null)
            {
                return BadRequest();
            }

            var mappedTags = model.Tags.Select(x => new Tag
            {
                Id = (int)x.Id,
                Name = x.Name
            }).ToList();
            var result = await _tagsRepository.SaveTagsOnLink(mappedTags, model.LinkId);
            return Json(new
            {
                Result = "OK"
            });
        }

        [HttpPost]
        public async Task<IActionResult> CreateOrGetTagByName([FromBody]string name)
        {
            if(string.IsNullOrEmpty(name))
            {
                return NotFound();
            }

            var tag = await _tagsRepository.CreateOrGetTagByName(name);

            return Json(tag);
        }


        private bool TagExists(int id)
        {
            return _context.Tags.Any(e => e.Id == id);
        }
    }
}
