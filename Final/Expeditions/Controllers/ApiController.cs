using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Expeditions.Models;

namespace Expeditions.Controllers
{
    [Route("api/{action}")]
    [ApiController]
    public class ApiController : Controller
    {
        private readonly ExpeditionsDbContext db;
        public ApiController(ExpeditionsDbContext context)
        {
            db = context;
        }
        [HttpGet]
        public IActionResult GetStats()
        {
            var exps = db.Expeditions.ToList().Count();
            var peaks = db.Peaks.ToList().Count();
            var unclimbed = db.Peaks
                .Where(e => e.Expeditions.ToList().Count() == 0)
                .ToList()
                .Count();
            return Json(new { totalExps = exps, totalPeaks = peaks, totalUnclimbed = unclimbed });
        }

    }
}
