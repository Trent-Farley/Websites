using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace HWSchV2.Models
{
    [Table("Detail")]
    public partial class Detail
    {
        public Detail()
        {
            Homework = new HashSet<Homework>();
        }

        [Key]
        public int Id { get; set; }
        public int Precedence { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime Duedate { get; set; }
        [Required]
        [StringLength(64)]
        public string Title { get; set; }
        [StringLength(512)]
        public string Description { get; set; }

        [InverseProperty("Info")]
        public virtual ICollection<Homework> Homework { get; set; }
    }
}
