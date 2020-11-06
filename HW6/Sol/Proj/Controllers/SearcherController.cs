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
            var info = new List<Dictionary<string, Album>>();
            var albums = _db.Albums
                .Where(alb => alb.Artist.ArtistId == s.ArtistId)
                .Include(t => t.Tracks)
                .ToList();
            foreach (var album in albums)
            {
                var item = _db.Tracks
                    .Where(tr => tr.Album.AlbumId == album.AlbumId)
                    .GroupBy(t => t.Genre.Name)
                    .OrderByDescending(c => c.Count())
                    .Select(k => k.Key)
                    .First();
                info.Add(new Dictionary<string, Album>() { { item, album } });
            }

            albInfo.Info = info;
            albInfo.ArtistName = _db.Artists.Find(s.ArtistId).Name;
            return View("Albums", albInfo);
        }
    }
}