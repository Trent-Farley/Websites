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
        private ChinookDbContext db;

        public HomeController(ILogger<HomeController> logger, ChinookDbContext context)
        {
            db = context;
            _logger = logger;
        }

        public IActionResult Index(SearchResult s)
        {
            return View("Index", s);
        }

        [HttpGet]
        public IActionResult SearchAlbums(SearchResult s)
        {
            s.AlbumResult = db.Albums.Where(alb => alb.Artist.Name.Contains(s.Search)).ToList();

            foreach (var val in s.AlbumResult)
            {
                _logger.LogInformation($"Val inside of s: {val.Title}");
            }
            return View("SearchAlbums", s);
        }

        [HttpPost]
        public IActionResult Search(SearchResult s)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("SearchAlbums", s);
            }
            else
            {
                return View("Index", s);
            }
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