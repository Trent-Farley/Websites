using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Proj.Models;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Proj.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private ChinookDbContext _db;

        public HomeController(ILogger<HomeController> logger, ChinookDbContext context)
        {
            _db = context;
            _logger = logger;
        }

        public IActionResult Index(SearchResult s)
        {
            return View("Index", s);
        }

        [HttpPost]
        public IActionResult Search(SearchResult s)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("SearchArtists", "Searcher", s);
            }
            else
            {
                return View("Index", s);
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}