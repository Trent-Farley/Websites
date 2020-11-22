using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace HWSchV2.Models
{
    [Table("Course")]
    public partial class Course
    {
        public Course()
        {
            Homework = new HashSet<Homework>();
        }

        [Key]
        public int Id { get; set; }
        [Required]
        [StringLength(6)]
        public string Department { get; set; }
        public int CourseNumber { get; set; }

        [InverseProperty("Class")]
        public virtual ICollection<Homework> Homework { get; set; }
    }
}
