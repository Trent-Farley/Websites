using System;
using System.Collections.Generic;

#nullable disable

namespace HWScheduler.Models
{
    public partial class Homework
    {
        public Homework()
        {
            HomeworkTags = new HashSet<HomeworkTag>();
        }

        public int Id { get; set; }
        public int? ClassId { get; set; }
        public int Precedence { get; set; }
        public DateTime Duedate { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool? Done { get; set; }

        public virtual Course Class { get; set; }
        public virtual ICollection<HomeworkTag> HomeworkTags { get; set; }
    }
}
