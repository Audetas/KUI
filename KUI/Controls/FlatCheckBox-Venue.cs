using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KUI.Controls
{
    public class FlatCheckBox : ControlBase
    {
        public bool Checked = false;

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            pevent.Graphics.FillRectangle(MouseOver
                ? new SolidBrush(Theme.ForeColor.Shade(Theme.ShadowSize, 0))
                : Theme.BackBrush, ClientRectangle);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            if (MouseOver)
                e.Graphics.FillRectangle(new SolidBrush(Theme.ForeColor.Shade(Theme.ShadowSize, 0)), ClientRectangle);

            e.Graphics.DrawRectangle(Theme.FontPen, 1, 1, Height - 2, Height - 2);

            if (Checked)
                e.Graphics.FillRectangle(Theme.FontBrush, 4, 4, Height - 7, Height - 7);

            SizeF textSize = e.Graphics.MeasureString(Text, Theme.BodyFont);
            e.Graphics.DrawString(Text, Theme.BodyFont, Theme.FontBrush, 
                Height + 3, Height / 2 - textSize.Height / 2);
        }


        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            Checked = !Checked;
            Invalidate();
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
