using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Expeditions.Models;
using Expeditions.ViewModels;

namespace Expeditions.Controllers
{
    public class ExpeditionsController : Controller
    {
        private readonly ExpeditionsDbContext db;

        public ExpeditionsController(ExpeditionsDbContext context)
        {
            db = context;
        }

        // GET: Expeditions
        public async Task<IActionResult> Index()
        {
            ViewData["PeakId"] = new SelectList(db.Peaks, "Id", "Name");
            var expeditionsDbContext = db.Expeditions
                .Include(e => e.Peak)
                .Include(e => e.TrekkingAgency);
            var newHike = new Hike()
            {
                Hikes = await expeditionsDbContext.ToListAsync(),
                Mountains = await db.Peaks.ToListAsync()
            };
            return View(newHike);
        }
        public IActionResult SortDate()
        {
            ViewData["PeakId"] = new SelectList(db.Peaks, "Id", "Name");

            var expeditions = db.Expeditions
                .OrderByDescending(t => t.StartDate)
                .Include(p => p.Peak)
                .Include(t => t.TrekkingAgency)
                .ToList();
            expeditions.Reverse();
            var newHike = new Hike()
            {
                Hikes = expeditions,
                Mountains = db.Peaks.ToList()
            };
            return View("Index", newHike);
        }
        public IActionResult SortPeak()
        {
            ViewData["PeakId"] = new SelectList(db.Peaks, "Id", "Name");

            var expeditions = db.Expeditions
                .OrderByDescending(t => t.Peak.Name)
                .Include(p => p.Peak)
                .Include(t => t.TrekkingAgency)
                .ToList();
            expeditions.Reverse();
            var newHike = new Hike()
            {
                Hikes = expeditions,
                Mountains = db.Peaks.ToList()
            };
            return View("Index", newHike);
        }

        public IActionResult MountainSort(Hike hike)
        {
            ViewData["PeakId"] = new SelectList(db.Peaks, "Id", "Name");
            if (hike.Mountain.Id == null)
            {
                return NotFound();
            }

            var expeditions = db.Expeditions
                 .Where(p => p.Peak.Id == hike.Mountain.Id)
                .Include(p => p.Peak)
                .Include(t => t.TrekkingAgency)
                .ToList();
            var newHike = new Hike()
            {
                Hikes = expeditions,
                Mountains = db.Peaks.ToList()
            };
            return View("Index", newHike);
        }

        // GET: Expeditions/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expedition = await db.Expeditions
                .Include(e => e.Peak)
                .Include(e => e.TrekkingAgency)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expedition == null)
            {
                return NotFound();
            }

            return View(expedition);
        }

        // GET: Expeditions/Create
        public IActionResult Create()
        {
            ViewData["PeakId"] = new SelectList(db.Peaks, "Id", "Name");
            ViewData["TrekkingAgencyId"] = new SelectList(db.TrekkingAgencies, "Id", "Name");


            return View();
        }

        // POST: Expeditions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Season,Year,StartDate,TerminationReason,OxygenUsed,PeakId,TrekkingAgencyId")] Expedition expedition)
        {
            if (ModelState.IsValid)
            {
                db.Add(expedition);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["PeakId"] = new SelectList(db.Peaks, "Id", "Name", expedition.PeakId);
            ViewData["TrekkingAgencyId"] = new SelectList(db.TrekkingAgencies, "Id", "Id", expedition.TrekkingAgencyId);
            return View(expedition);
        }

        // GET: Expeditions/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expedition = await db.Expeditions.FindAsync(id);
            if (expedition == null)
            {
                return NotFound();
            }
            ViewData["PeakId"] = new SelectList(db.Peaks, "Id", "Name", expedition.PeakId);
            ViewData["TrekkingAgencyId"] = new SelectList(db.TrekkingAgencies, "Id", "Id", expedition.TrekkingAgencyId);
            return View(expedition);
        }

        // POST: Expeditions/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Season,Year,StartDate,TerminationReason,OxygenUsed,PeakId,TrekkingAgencyId")] Expedition expedition)
        {
            if (id != expedition.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(expedition);
                    await db.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpeditionExists(expedition.Id))
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
            ViewData["PeakId"] = new SelectList(db.Peaks, "Id", "Name", expedition.PeakId);
            ViewData["TrekkingAgencyId"] = new SelectList(db.TrekkingAgencies, "Id", "Id", expedition.TrekkingAgencyId);
            return View(expedition);
        }

        // GET: Expeditions/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var expedition = await db.Expeditions
                .Include(e => e.Peak)
                .Include(e => e.TrekkingAgency)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (expedition == null)
            {
                return NotFound();
            }

            return View(expedition);
        }

        // POST: Expeditions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var expedition = await db.Expeditions.FindAsync(id);
            db.Expeditions.Remove(expedition);
            await db.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ExpeditionExists(int id)
        {
            return db.Expeditions.Any(e => e.Id == id);
        }
    }
}
