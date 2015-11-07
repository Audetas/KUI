using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KUI.Controls
{
    public class FlatForeSeperator : FlatSeperator
    {
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            pevent.Graphics.FillRectangle(Theme.ForeBrush, pevent.ClipRectangle);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Horizontal)
                e.Graphics.DrawLine(Theme.BackPen, 0, Height / 2, Width, Height / 2);
            else
                e.Graphics.DrawLine(Theme.BackPen, Width / 2, 0, Width / 2, Height);
        }
    }
}
