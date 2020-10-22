using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using HW4Proj.Models;
using System.Drawing;
namespace HW4Proj.Controllers
{
     public class ColorInterpolation : Controller
    {
        private readonly ILogger<ColorInterpolation> _logger;

        public ColorInterpolation(ILogger<ColorInterpolation> logger)
        {
            _logger = logger;
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}