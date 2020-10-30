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
            //foreach (var val in db.Homeworks.ToList())
            //{
            //    _logger.LogInformation(val.ToString());
            //}
            return View();
        }

        [HttpPost]
        public IActionResult Schedule(Homework hw)
        {
            if (ModelState.IsValid)
            {
                db.Add(hw);
                db.SaveChanges();
                return RedirectToAction("Assignments", true);
            }
            else
            {
                return View("Index", hw);
            }
        }

        [HttpGet]
        public IActionResult Assignments(bool priority = false, bool duedate = false)
        {
            var assignments = new List<Homework>();
            if (priority)
            {
                assignments = db.Homeworks.OrderByDescending(db => db.Precedence).ToList();
                return View("Assignments", assignments);
            }
            else if (duedate)
            {
                //Order by furthest out to closest in days
                assignments = db.Homeworks.OrderByDescending(db => db.DueDate).ToList();
                //Reverse list to show the item that is due soon
                assignments.Reverse();
                return View("Assignments", assignments);
            }
            else
            {
                assignments = db.Homeworks.ToList();
                return View("Assignments", assignments);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}