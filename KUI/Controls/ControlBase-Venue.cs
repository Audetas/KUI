using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KUI
{
    public class ControlBase : Control
    {
        public bool HasShadow = false;
        public int ShadowLevel = 0;
        public bool MouseOver = false;
        public bool IsMouseDown = false;

        private Timer _ticker = new Timer();

        public ControlBase()
        {
            DoubleBuffered = true;
            _ticker.Interval = 16;
            _ticker.Tick += _ticker_Tick;
        }

        public virtual Rectangle ShadeRect(int index)
        {
            return new Rectangle(Location.X - index, Location.Y - index, Width + index * 2, Height + index * 2);
        }

        public virtual void DrawShadow(Graphics g)
        {
            if (HasShadow)
            {
                for (int i = 0; i < ShadowLevel; i++)
                {
                    g.DrawRectangle(
                        new Pen(Theme.ForeColor.Shade(Theme.ShadowSize, i)),
                        ShadeRect(i));
                }
            }
        }

        private void _ticker_Tick(object sender, EventArgs e)
        {
            ShadowLevel++;

            if (ShadowLevel >= Theme.ShadowSize || ShadowLevel == 0)
                _ticker.Stop();

            Parent.Invalidate(ShadeRect(Theme.ShadowSize), false);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);
            IsMouseDown = true;
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);
            IsMouseDown = false;
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            MouseOver = true;
            ShadowLevel = 1;
            _ticker.Start();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            MouseOver = false;
            ShadowLevel = 0;
        }
    }
}
