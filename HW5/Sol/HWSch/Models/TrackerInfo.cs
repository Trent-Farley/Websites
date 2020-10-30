using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWSch.Models
{
    public class TrackerInfo
    {
        public IEnumerable<Homework> Homeworks { get; set; }
        public int HWId { get; set; }
        public bool Priority { get; set; }
        public bool DueDate { get; set; }
    }
}