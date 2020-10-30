using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HWSch.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace HWSch.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private HWScheduleDBContext db;

        public HomeController(ILogger<HomeController> logger, HWScheduleDBContext context)
        {
            db = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Schedule(Homework hw)
        {
            if (ModelState.IsValid)
            {
                db.Add(hw);
                db.SaveChanges();
                return RedirectToAction("Assignments");
            }
            else
            {
                return View("Index", hw);
            }
        }

        [HttpGet]
        public IActionResult Assignments(TrackerInfo tracker)
        {
            var assignments = new List<Homework>();
            if (tracker.Priority)
            {
                tracker.Homeworks = db.Homeworks.OrderByDescending(db => db.Precedence).ToList();
                return View("Assignments", tracker);
            }
            else if (tracker.DueDate)
            {
                //Order by furthest out to closest in days
                tracker.Homeworks = db.Homeworks.OrderByDescending(db => db.DueDate).ToList();
                //Reverse list to show the item that is due soon
                tracker.Homeworks.Reverse();
                return View("Assignments", tracker);
            }
            else
            {
                tracker.Homeworks = db.Homeworks.ToList();
                return View("Assignments", tracker);
            }
        }

        [HttpPost]
        public IActionResult SortBy(TrackerInfo tracker)
        {
            return RedirectToAction("Assignments", tracker);
        }

        [HttpPost]
        public IActionResult SetFin(TrackerInfo tracker)
        {
            db.Homeworks.Find(tracker.HWId).Fin = true;
            db.Homeworks.Update(db.Homeworks.Find(tracker.HWId));
            db.SaveChanges();
            return RedirectToAction("Assignments", tracker);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}