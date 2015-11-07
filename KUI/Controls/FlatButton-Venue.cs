using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KUI.Controls
{
    public class FlatButton : ControlBase
    {
        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(
                MouseOver ? Theme.AccentBrush : Theme.ForeBrush, ClientRectangle);

            SizeF textSize = e.Graphics.MeasureString(Text, Theme.BodyFont);
            e.Graphics.DrawString(Text, Theme.BodyFont, Theme.FontBrush, 
                Width / 2 - textSize.Width / 2, Height / 2 - textSize.Height / 2);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            MouseOver = false;
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            MouseOver = true;
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
