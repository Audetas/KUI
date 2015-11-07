using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KUI
{
    public static class Extensions
    {
        public static Color Shade(this Color baseColor, int shades, int index)
        {
            int delta = 200 - (index * (255 / shades));
            return Color.FromArgb(Math.Max(0, delta), baseColor);
        }

        public static string Between(this string s, string start, string end)
        {
            return s.Split(new string[] { start }, StringSplitOptions.None)[1]
                    .Split(new string[] { end }, StringSplitOptions.None)[0];
        }
    }
}
