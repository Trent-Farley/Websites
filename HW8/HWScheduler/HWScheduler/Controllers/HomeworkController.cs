using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HWScheduler.Models;

namespace HWScheduler.Controllers
{
    public class HomeworkController : Controller
    {
        private readonly HWDbContext _context;

        public HomeworkController(HWDbContext context)
        {
            _context = context;
        }

        // GET: Homework
        public async Task<IActionResult> Index()
        {
            var hWDbContext = _context.Homework.Include(h => h.Class).Include(h => h.Line);
            return View(await hWDbContext.ToListAsync());
        }

        // GET: Homework/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homework = await _context.Homework
                .Include(h => h.Class)
                .Include(h => h.Line)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homework == null)
            {
                return NotFound();
            }

            return View(homework);
        }

        // GET: Homework/Create
        public IActionResult Create()
        {
            ViewData["ClassId"] = new SelectList(_context.Courses, "Id", "Name");
            ViewData["LineId"] = new SelectList(_context.Tags, "Id", "Tagname");
            return View();
        }

        // POST: Homework/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ClassId,InfoId,Done,LineId")] Homework homework)
        {
            if (ModelState.IsValid)
            {
                _context.Add(homework);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ClassId"] = new SelectList(_context.Courses, "Id", "ClassId", homework.ClassId);
            ViewData["LineId"] = new SelectList(_context.Tags, "Id", "Tagname", homework.LineId);
            return View(homework);
        }

        // GET: Homework/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homework = await _context.Homework.FindAsync(id);
            if (homework == null)
            {
                return NotFound();
            }
            ViewData["ClassId"] = new SelectList(_context.Courses, "Id", "Department", homework.ClassId);
            ViewData["LineId"] = new SelectList(_context.Tags, "Id", "Tagname", homework.LineId);
            return View(homework);
        }

        // POST: Homework/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ClassId,InfoId,Done,LineId")] Homework homework)
        {
            if (id != homework.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(homework);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!HomeworkExists(homework.Id))
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
            ViewData["ClassId"] = new SelectList(_context.Courses, "Id", "Department", homework.ClassId);
            ViewData["LineId"] = new SelectList(_context.Tags, "Id", "Tagname", homework.LineId);
            return View(homework);
        }

        // GET: Homework/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var homework = await _context.Homework
                .Include(h => h.Class)
                .Include(h => h.Line)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (homework == null)
            {
                return NotFound();
            }

            return View(homework);
        }

        // POST: Homework/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var homework = await _context.Homework.FindAsync(id);
            _context.Homework.Remove(homework);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool HomeworkExists(int id)
        {
            return _context.Homework.Any(e => e.Id == id);
        }
    }
}