using System;
using System.Collections.Generic;
using System.Drawing;
using HW4Proj.Controllers;
using Microsoft.Extensions.Logging;

namespace HW4Proj.Tools
{
    public class ColorInterpolator
    {

        public IEnumerable<string> Interpolate(string color1, string color2, int numberOfColors)
        {
            var firstColor = ColorTranslator.FromHtml(color1);
            var secondColor = ColorTranslator.FromHtml(color2);
            var listOfColors = new List<string>();
            var hsv1 = HSVConverter.ColorToHSV(firstColor);
            var hsv2 = HSVConverter.ColorToHSV(secondColor);
            foreach(var color in LinearInterpolation(hsv1,hsv2,numberOfColors))
            {
                listOfColors.Add(ColorTranslator.ToHtml(color));
            }
            return listOfColors;
        }
        private IEnumerable<Color> LinearInterpolation(Dictionary<string, double> hsv1, Dictionary<string, double> hsv2, int numberOfColors)
        {
            /* I found this general formula: v = v0 + ratio * (v1 - v0) Which I believe we need to use for each channel of hsv. I did this in a for loop of percentages (i.e. 0.1, 0.2... I also tried 0.01, 0.02...) to get the colors, but halfway through it breaks a single color. The formula is described as such: given a start value v0, an end value v1 and the required ratio (a normalized float between 0.0 and 1.0) */
            var colors = new List<Color>();
            for(var change = 0; change < numberOfColors; ++change)
            {
                string percentage = $"0.{change}";
                double steps = double.Parse(percentage);
                var newHue = hsv1["hue"] + steps * (hsv2["hue"] - hsv1["hue"]);
                var newSat = hsv1["saturation"] + steps * (hsv2["saturation"] - hsv1["saturation"]);
                var newVal = hsv1["value"] + steps * (hsv2["value"] - hsv1["value"]);
                colors.Add(HSVConverter.ColorFromHSV(newHue, newSat, newVal));
            }
            return colors;
        }
    }
}