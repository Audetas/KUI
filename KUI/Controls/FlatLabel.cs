using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KUI.Controls
{
    public class FlatLabel : ControlBase
    {
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            pevent.Graphics.FillRectangle(Theme.BackBrush, pevent.ClipRectangle);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawString(Text, Theme.BodyFont, Theme.FontBrush, 0, 0);
        }
    }
}
