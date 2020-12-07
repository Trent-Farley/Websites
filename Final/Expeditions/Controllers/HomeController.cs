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
            var hike = new Hike()
            {
                Hikes = db.Expeditions.Include(e => e.Peak).Include(e => e.TrekkingAgency).ToList()
            };
            return View(hike);
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
