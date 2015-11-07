using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KUI.Controls
{
    public class FlatForeLabel : FlatLabel
    {
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            pevent.Graphics.FillRectangle(Theme.ForeBrush, pevent.ClipRectangle);
        }
    }
}
