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
    public class SearcherController : Controller
    {
        private readonly ILogger<SearcherController> _logger;
        private ChinookDbContext _db;

        public SearcherController(ILogger<SearcherController> logger, ChinookDbContext context)
        {
            _logger = logger;
            _db = context;
        }

        [HttpGet]
        public IActionResult SearchAlbums(SearchResult s)
        {
            s.AlbumResult = _db.Albums
                .Where(alb => alb.Artist.Name
                .Contains(s.Search))
                .Include(a => a.Artist)
                .Include(t => t.Tracks)
                .ToList();

            foreach (var val in s.AlbumResult)
            {
                _logger.LogInformation($"Val inside of s: {val.Title}");
            }
            return View("SearchAlbums", s);
        }
    }
}