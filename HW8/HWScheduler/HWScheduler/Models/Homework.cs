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
        public int? InfoId { get; set; }
        public bool? Done { get; set; }
        public int? LineId { get; set; }

        [ForeignKey(nameof(ClassId))]
        [InverseProperty(nameof(Course.Homework))]
        public virtual Course Class { get; set; }
        [ForeignKey(nameof(InfoId))]
        [InverseProperty(nameof(Detail.Homework))]
        public virtual Detail Info { get; set; }
        [ForeignKey(nameof(LineId))]
        [InverseProperty(nameof(Tag.Homework))]
        public virtual Tag Line { get; set; }
    }
}
