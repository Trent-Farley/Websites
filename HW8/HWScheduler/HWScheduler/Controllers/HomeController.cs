﻿using System;
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
        public IActionResult Create()
        {
            ViewData["Classes"] = new SelectList(db.Courses, "Id", "Name");
            ViewData["Tags"] = db.Tags.ToList();
            return View();
        }


        [HttpPost]
        public IActionResult Create(Homework hw)
        {
            if (ModelState.IsValid)
            {
                db.Add(hw);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewData["Classes"] = new SelectList(db.Courses, "Id", "Name");
            return View(hw);
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