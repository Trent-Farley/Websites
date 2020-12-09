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


    }
}
