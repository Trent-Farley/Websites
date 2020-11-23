using System;
using System.Collections.Generic;

#nullable disable

namespace HWScheduler.Models
{
    public partial class Tag
    {
        public Tag()
        {
            Homework = new HashSet<Homework>();
        }

        public int Id { get; set; }
        public string Tagname { get; set; }

        public virtual ICollection<Homework> Homework { get; set; }
    }
}
