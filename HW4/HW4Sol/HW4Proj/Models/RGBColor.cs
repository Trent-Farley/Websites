using System.ComponentModel.DataAnnotations;
using System;
namespace HW4Proj.Models
{
    public class RGBColor
    {
        [Range(0,255)]
        public int? Red { get; set; }   
        [Range(0,255)]
        public int? Green { get; set; }
        [Range(0,255)]
        public int? Blue { get; set; }
        /* Found this here:
        https://stackoverflow.com/questions/2395438/convert-system-drawing-color-to-rgb-and-hex-value 
        was trying use Color.Name but this way works better */

        public string Hex {get => $"#{Red:X2}{Green:X2}{Blue:X2}"; }
    }
}
