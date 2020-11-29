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
using Microsoft.AspNetCore.Mvc.Rendering;

namespace HWScheduler.Controllers
{
    [Route("/api/{action}")]
    public class ApiHomeworkController : Controller
    {
        private readonly HWDbContext db;

        public ApiHomeworkController(HWDbContext context)
        {
            db = context;
        }

        public IActionResult GetTags()
        {
            List<Tag> tags = db.Tags.ToList();
            return Json(tags);
        }

        public void AddTag(string tagname)
        {

            var newTag = new Tag
            {
                Tagname = tagname
            };
            db.Tags.Add(newTag);
            db.SaveChanges();
        }
    }
}
