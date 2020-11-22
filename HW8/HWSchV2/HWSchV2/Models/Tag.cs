using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace HWSchV2.Models
{
    [Table("Tag")]
    public partial class Tag
    {
        public Tag()
        {
            Homework = new HashSet<Homework>();
        }

        [Key]
        public int Id { get; set; }
        [StringLength(40)]
        public string Tagname { get; set; }

        [InverseProperty("Line")]
        public virtual ICollection<Homework> Homework { get; set; }
    }
}
