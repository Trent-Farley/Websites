using HWScheduler.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HWScheduler.ViewModels
{
    public class HomeworkList
    {
        public IEnumerable<Homework> Homeworks { get; set; }
    }
}