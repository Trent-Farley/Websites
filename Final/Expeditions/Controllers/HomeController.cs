using Expeditions.Models;
using Expeditions.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Expeditions.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ExpeditionsDbContext db;
        public HomeController(ILogger<HomeController> logger, ExpeditionsDbContext context)
        {
            db = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            var newMtn = new Mountains()
            {
                Mtns = db.Peaks
                .OrderByDescending(h => h.Height)
                .Take(15)
                .Include(e => e.Expeditions)
                .ToList()
            };
            return View(newMtn);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
