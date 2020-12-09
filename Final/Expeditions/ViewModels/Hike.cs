using Expeditions.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Expeditions.ViewModels
{
    public class Hike
    {
        public IEnumerable<Expedition> Hikes { get; set; }
        public IEnumerable<Peak> Mountains { get; set; }
        public Peak Mountain { get; set; }
    }
}
