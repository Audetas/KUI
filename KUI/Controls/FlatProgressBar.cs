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
    public class FlatProgressBar : ControlBase
    {
        private int _progress = 0;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int Progress
        {
            get { return _progress; }
            set { _progress = value; Invalidate(); }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            pevent.Graphics.FillRectangle(Theme.BackBrush, ClientRectangle);

            if (MouseOver)
                pevent.Graphics.FillRectangle(
                    new SolidBrush(Theme.ForeColor.Shade(Theme.ShadowSize, 0)), ClientRectangle);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Theme.FontPen, 1, 1, Width - 2, Height - 2);
            e.Graphics.FillRectangle(Theme.AccentBrush, 5, 5,
                (Width - 10) * (_progress / 100f), Height - 9);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            HasShadow = true;
            Parent.Invalidate(ShadeRect(Theme.ShadowSize), false);
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            HasShadow = false;
            Parent.Invalidate(ShadeRect(Theme.ShadowSize), false);
            Invalidate();
        }
    }
}
