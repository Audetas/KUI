using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KUI.Windows
{
    public class FlatToolFrame : FlatFrame
    {
        public FlatToolFrame()
        {
            Padding = new Padding(0, 30, 0, 25);
            // TOOD SHADOWS ON BARS
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Theme.AccentBrush, 0, 0, Width, 29);
            e.Graphics.DrawString(Text, Theme.HeaderFont, Theme.FontBrush, 4, 6);
            e.Graphics.FillRectangle(Theme.ForeBrush, 0, Height - 29, Width, 29);
        }
    }
}