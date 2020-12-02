using System;
using System.Collections.Generic;

#nullable disable

namespace HWScheduler.Models
{
    public partial class Tag
    {
        public Tag()
        {
            HomeworkTags = new HashSet<HomeworkTag>();
        }

        public int Id { get; set; }
        public string Tagname { get; set; }

        public virtual ICollection<HomeworkTag> HomeworkTags { get; set; }
    }
}
