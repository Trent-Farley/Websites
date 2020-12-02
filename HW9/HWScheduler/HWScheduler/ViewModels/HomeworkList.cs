using HWScheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWScheduler.ViewModels
{
    public class HomeworkList
    {
        public IEnumerable<Course> Courses { get; set; }
        public IEnumerable<Homework> Assignments { get; set; }

        public bool CourseList { get; set; }
    }
}