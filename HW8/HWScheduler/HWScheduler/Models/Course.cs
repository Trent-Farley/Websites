using System;
using System.Collections.Generic;

#nullable disable

namespace HWScheduler.Models
{
    public partial class Course
    {
        public Course()
        {
            Homework = new HashSet<Homework>();
        }

        public int Id { get; set; }
        public string Department { get; set; }
        public int CourseNumber { get; set; }

        public virtual ICollection<Homework> Homework { get; set; }
    }
}
