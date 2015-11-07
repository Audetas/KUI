using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KUI.Controls
{
    public class FlatSeperator : ControlBase
    {
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public bool Horizontal { get; set; }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            pevent.Graphics.FillRectangle(Theme.BackBrush, pevent.ClipRectangle);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (Horizontal)
                e.Graphics.DrawLine(Theme.ForePen, 0, Height / 2, Width, Height / 2);
            else
                e.Graphics.DrawLine(Theme.ForePen, Width / 2, 0, Width / 2, Height);
        }
        
        protected override void OnMouseEnter(EventArgs e)
        {
        }

        protected override void OnMouseLeave(EventArgs e)
        {
        }
    }
}
