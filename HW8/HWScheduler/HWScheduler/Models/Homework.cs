using System;
using System.Collections.Generic;

#nullable disable

namespace HWScheduler.Models
{
    public partial class Homework
    {
        public int Id { get; set; }
        public int? ClassId { get; set; }
        public int? InfoId { get; set; }
        public bool? Done { get; set; }
        public int? LineId { get; set; }

        public virtual Course Class { get; set; }
        public virtual Detail Info { get; set; }
        public virtual Tag Line { get; set; }
    }
}
