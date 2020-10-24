using System;
using System.Collections.Generic;
using System.Drawing;

namespace HW4Proj.Tools
{
    public class ColorInterpolator
    {
        public List<string> Interpolate(string color1, string color2, int numberOfColors)
        {
            Color firstColor = ColorTranslator.FromHtml(color1);
            Color secondColor = ColorTranslator.FromHtml(color2);
            var listOfColors = new List<string>();
            var start = HSVConverter.ColorToHSV(firstColor);
            var end = HSVConverter.ColorToHSV(secondColor);
            foreach(var color in LinearInterpolation(start,end,numberOfColors))
            {
                listOfColors.Add(ColorTranslator.ToHtml(color));
            }
            return listOfColors;
        }
        private IEnumerable<Color> LinearInterpolation(Dictionary<string, double> start, Dictionary<string, double> end, int numberOfColors)
        {
            var colors = new List<Color>();
            var newHue = start["hue"];
            var newSat = start["saturation"];
            var newVal = start["value"];
            var hueStep = (end["hue"] - start["hue"])/(numberOfColors - 1);
            var satStep = (end["saturation"] - start["saturation"])/(numberOfColors - 1);
            var valStep = (end["value"] - start["value"])/(numberOfColors - 1);

            for(var i = 0; i < numberOfColors; i++)
            {
                newHue = start["hue"] + (i * hueStep);
                newSat = start["saturation"] + (i * satStep);
                newVal = start["value"] + (i * valStep);  
                colors.Add(HSVConverter.ColorFromHSV(newHue, newSat, newVal));        
            }

            return colors;
        }
    }
}