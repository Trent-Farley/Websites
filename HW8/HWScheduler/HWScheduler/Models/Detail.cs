using System;
using System.Collections.Generic;

#nullable disable

namespace HWScheduler.Models
{
    public partial class Detail
    {
        public Detail()
        {
            Homework = new HashSet<Homework>();
        }

        public int Id { get; set; }
        public int Precedence { get; set; }
        public DateTime Duedate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Homework> Homework { get; set; }
    }
}
