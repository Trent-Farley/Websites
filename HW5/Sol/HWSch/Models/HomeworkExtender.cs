using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HWSch.Models
{
    public partial class Homework
    {
        public override string ToString()
        {
            return base.ToString() + $"\n \n {Id}\n {Title} : {Course} ";
        }
    }
}