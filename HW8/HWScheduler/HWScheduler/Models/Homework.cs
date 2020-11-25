using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace HWScheduler.Models
{
    public partial class Homework
    {
        [Key]
        public int Id { get; set; }
        public int? ClassId { get; set; }
        public int? LineId { get; set; }
        public int Precedence { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Duedate { get; set; }
        [Required]
        [StringLength(64)]
        public string Title { get; set; }
        [StringLength(512)]
        public string Description { get; set; }
        public bool? Done { get; set; }

        [ForeignKey(nameof(ClassId))]
        [InverseProperty(nameof(Course.Homework))]
        public virtual Course Class { get; set; }
        [ForeignKey(nameof(LineId))]
        [InverseProperty(nameof(Tag.Homework))]
        public virtual Tag Line { get; set; }
    }
}
