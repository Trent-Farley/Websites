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
using Newtonsoft.Json.Linq;

namespace HWScheduler.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly HWDbContext db;

        public HomeController(ILogger<HomeController> logger, HWDbContext context)
        {
            _logger = logger;
            db = context;
        }

        public IActionResult Index() =>
            View("Index", new HomeworkList()
            {
                Assignments = db.Homework
                .Include(h => h.Class)
                .Include(t => t.HomeworkTags)
                .ThenInclude(tn => tn.Tag),
                Courses = db.Courses.ToList(),
                CourseList = false
            });
        public IActionResult CourseList(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            IEnumerable<Homework> homeworks = db.Homework
                .Where(h => h.Class.Id == id)
                .Include(t => t.HomeworkTags)
                .ThenInclude(tn => tn.Tag)
                .ToList();
            return View("Index", new HomeworkList()
            {
                Assignments = homeworks,
                Courses = db.Courses.ToList(),
                CourseList = true
            });
        }


        public IActionResult AssignmentDone(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            db.Homework.Find(id).Done = true;
            db.Homework.Update(db.Homework.Find(id));
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
        [HttpGet]
        public IActionResult Create()
        {
            ViewData["Classes"] = new SelectList(db.Courses, "Id", "Name");
            ViewData["Tags"] = db.Tags.ToList();
            return View();
        }

        public IActionResult AddHw(Homework hw, IEnumerable<string> tagIds)
        {
            if (ModelState.IsValid)
            {
                db.Add(hw);
                db.SaveChanges();


                //var id = int.Parse(new string(t.Where(c => char.IsDigit(c)).ToArray()));
                var id = db.Tags.OrderByDescending(r => r.Id).FirstOrDefault().Id;

                var tempHWT = new HomeworkTag
                {
                    Tag = db.Tags.Where(t => t.Id == id).Single(),
                    TagId = db.Tags.Where(t => t.Id == id).Single().Id,
                    HomeworkId = hw.Id,
                    Homework = hw
                };
                _logger.LogInformation(tempHWT.TagId.ToString());
                db.HomeworkTags.Add(tempHWT);


                db.SaveChanges();
                return Json(new { success = true });
            }
            var errors = new List<string>();
            foreach (var modelstate in ViewData.ModelState.Values)
            {
                foreach (var err in modelstate.Errors)
                {
                    errors.Append(err.ToString());
                }
            }
            return Json(new { success = false, errors });
        }

        [HttpPost]
        public IActionResult Create(string hw)
        {
            var assignment = GetHomework(hw);
            return RedirectToAction("AddHw", assignment);
        }

        private Homework GetHomework(string hw)
        {
            var data = JObject.Parse(hw);
            var newHw = new Homework
            {
                ClassId = int.Parse(data["Class"].ToString()),
                Precedence = int.Parse(data["Priority"].ToString()),
                Duedate = DateTime.Parse(data["DueDate"].ToString()),
                Title = data["Title"].ToString(),
                Description = data["Note"].ToString()
            };
            // Get tagIds to AddHW function. 
            var tagIds = data["Tags"].ToString()
                            .Replace('[', ' ')
                            .Replace(']', ' ').Split(',');


            return newHw;
        }

        public IActionResult GetTags()
        {
            List<Tag> tags = db.Tags.ToList();
            return Json(tags);
        }

        public IActionResult AddClasses()
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddClasses(string classes)
        {
            foreach (var course in classes.Split(','))
            {

                var tmp = new Course
                {
                    Name = course
                };
                if (!db.Courses.Where(c => c.Name == tmp.Name).Any())
                {
                    db.Courses.Add(tmp);
                }
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}