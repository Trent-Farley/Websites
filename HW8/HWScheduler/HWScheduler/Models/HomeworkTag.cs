using System;
using System.Collections.Generic;

#nullable disable

namespace HWScheduler.Models
{
    public partial class HomeworkTag
    {
        public int HomeworkId { get; set; }
        public int TagId { get; set; }

        public virtual Homework Homework { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
