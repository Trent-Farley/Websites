using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace HW4Proj.Models
{
    public class ColorInterpolation
    {
        [RegularExpression("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$", ErrorMessage="Please enter a hex number i.e. #000000 to #FFFFFF"), Required]
        public string FirstColor { get; set; }
        
        [RegularExpression("^#([A-Fa-f0-9]{6}|[A-Fa-f0-9]{3})$", ErrorMessage="Please enter a hex number i.e. #000000 to #FFFFFF"), Required]
        public string SecondColor {get; set;}
        
        [Required]
        public int? NumberOfColors{get; set;}

        public IEnumerable<string> Interpolations {get; set;}
    }
}