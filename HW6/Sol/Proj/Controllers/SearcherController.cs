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
        public IActionResult SearchArtists(SearchResult s)
        {
            s.ArtistResult = _db.Artists
                .Where(a => a.Name.Contains(s.Search))
                .ToList();

            return View("SearchArtists", s);
        }

        [HttpGet]
        public IActionResult AlbumTrack(SearchResult s)
        {
            var albInfo = new AlbumInfo();
            albInfo.AlbumsTracks = _db.Albums
                .Where(alb => alb.Artist.ArtistId == s.ArtistId)
                .Include(t => t.Tracks)
                .ToList();
            return View("Albums", albInfo);
        }
    }
}