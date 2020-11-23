using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HWScheduler.Models;
using HWScheduler.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HWScheduler.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HWDbContext db;

        public HomeController(ILogger<HomeController> logger, HWDbContext context)
        {
            _logger = logger;
            db = context;
        }

        public IActionResult Index()
        {
            var test = new HomeworkList
            {
                Assignments = db.Homework
                .Include(h => h.Class)
                .Include(h => h.Info)
                .Include(h => h.Line).ToList()
            };
            return View(test);
        }

        public IActionResult ListClasses(int classId)
        {
            Console.WriteLine($"Class Id: {classId}");
            return View("ClassHws");
        }
        public IActionResult AssignmentDone(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            Console.WriteLine($"Hw id: {id}");
            db.Homework.Find(id).Done = true;
            db.Homework.Update(db.Homework.Find(id));
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}