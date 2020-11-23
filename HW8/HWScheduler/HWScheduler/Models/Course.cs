using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace HWScheduler.Models
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
        [StringLength(25)]
        public string Name { get; set; }

        [InverseProperty("Class")]
        public virtual ICollection<Homework> Homework { get; set; }
    }
}
