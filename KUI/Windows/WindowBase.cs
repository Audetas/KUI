using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KUI
{
    public class WindowBase : Form
    {
        public WindowBase()
        {
            AutoScaleMode = AutoScaleMode.Dpi;
            FormBorderStyle = FormBorderStyle.None;
            DoubleBuffered = true;
        }

        public virtual void DrawShadow(Graphics g)
        {
            for (int i = 0; i < Theme.ShadowSize; i++)
            {
                g.DrawRectangle(
                    new Pen(Theme.ShadowColor.Shade(Theme.ShadowSize, i)),
                    ShadeRect(i));
            }
        }

        public virtual Rectangle ShadeRect(int index)
        {
            return new Rectangle(Location.X - index, Location.Y - index, Width + index * 2, Height + index * 2);
        }

        protected override void OnPaintBackground(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Theme.BackBrush, ClientRectangle);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            foreach (Control c in Controls)
            {
                if ((c is ControlBase || c is WindowBase) && (c as Control).Visible)
                    (c as dynamic).DrawShadow(e.Graphics);
            }
        }
    }
}
