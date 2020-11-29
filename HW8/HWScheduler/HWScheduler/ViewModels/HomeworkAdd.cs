using HWScheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWScheduler.ViewModels
{
    public class HomeworkAdd
    {
        public Homework Homework { get; set; }
        public IEnumerable<Tag> Tags { get; set; }

    }
}
