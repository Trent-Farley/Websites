using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Expeditions.Models;
using Expeditions.ViewModels;
using Microsoft.Extensions.Logging;

namespace Expeditions.Controllers
{
    public class ExpeditionsController : Controller
    {
        private readonly ExpeditionsDbContext db;
        private readonly ILogger<ExpeditionsController> _logger;
        public ExpeditionsController(ILogger<ExpeditionsController> logger, ExpeditionsDbContext context)
        {
            _logger = logger;
            db = context;
        }

        // GET: Expeditions
        public async Task<IActionResult> Index()
        {
            ViewData["PeakId"] = new SelectList(db.Peaks, "Id", "Name");
            var expeditionsDbContext = db.Expeditions
                .OrderByDescending(t => t.StartDate)
                .Take(50)
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
                .Take(50)
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
            _logger.LogInformation("I am here");

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
            var peaks = db.Peaks.OrderByDescending(n => n.Name).ToList();
            peaks.Reverse();
            var treksA = db.TrekkingAgencies.OrderByDescending(n => n.Name).ToList();
            treksA.Reverse();
            ViewData["PeakId"] = new SelectList(peaks, "Id", "Name");
            ViewData["TrekkingAgencyId"] = new SelectList(treksA, "Id", "Name");


            return View();
        }

        // POST: Expeditions/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,StartDate,TerminationReason,OxygenUsed,PeakId,TrekkingAgencyId")] Expedition expedition)
        {
            //Need to change year and season
            if (ModelState.IsValid)
            {
                expedition.Year = expedition.StartDate.Value.Year;
                expedition.Season = GetSeason(expedition.StartDate);
                _logger.LogInformation(expedition.Season);
                db.Add(expedition);
                await db.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            var peaks = db.Peaks.OrderByDescending(n => n.Name).ToList();
            peaks.Reverse();
            var treksA = db.TrekkingAgencies.OrderByDescending(n => n.Name).ToList();
            treksA.Reverse();
            ViewData["PeakId"] = new SelectList(peaks, "Id", "Name");
            ViewData["TrekkingAgencyId"] = new SelectList(treksA, "Id", "Name");

            return View(expedition);
        }

        private string GetSeason(DateTime? date)
        {
            //Credit to https://stackoverflow.com/questions/1579587/how-can-i-get-the-current-season-using-net-summer-winter-etc
            float value = (float)date.Value.Month + date.Value.Day / 100;
            if (value < 3.21 || value >= 12.22) return "Winter";
            if (value < 6.21) return "Spring";
            if (value < 9.23) return "Summer";
            return "Autumn";
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
