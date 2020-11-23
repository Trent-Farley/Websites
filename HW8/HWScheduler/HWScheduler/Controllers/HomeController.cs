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
        private readonly HWDbContext _context;

        public HomeController(ILogger<HomeController> logger, HWDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var test = new HomeworkList
            {
                Homeworks = _context.Homework
                .Include(h => h.Class)
                .Include(h => h.Info)
                .Include(h => h.Line).ToList()
            };
            return View(test);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}