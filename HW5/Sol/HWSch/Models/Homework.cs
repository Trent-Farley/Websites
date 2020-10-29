using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HWSch.Models
{
    public partial class Homework
    {
        [Key]
        [Column("ID")]
        public int Id { get; set; }

        public int Precedence { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime DueDate { get; set; }

        [Required]
        [StringLength(64)]
        public string Course { get; set; }

        [Required]
        [StringLength(64)]
        public string Title { get; set; }

        [StringLength(512)]
        public string Note { get; set; }
    }
}