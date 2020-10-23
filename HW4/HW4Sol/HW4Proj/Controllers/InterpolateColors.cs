using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HW4Proj.Models;
using System.Drawing;
using HW4Proj.Tools;
namespace HW4Proj.Controllers
{
     public class InterpolateColors : Controller
    {
        private readonly ILogger<InterpolateColors> _logger;

        public InterpolateColors(ILogger<InterpolateColors> logger)
        {
            _logger = logger;
        }
        public IActionResult Create()
        {

            return View();
        }
        [HttpPost]
        public IActionResult Create(ColorInterpolation ci)
        {
            var interpolate = new ColorInterpolator();
            ci.Interpolations = interpolate.Interpolate(ci.FirstColor, ci.SecondColor,(int) ci.NumberOfColors);
            return View("Create", ci);
        }
    }
}