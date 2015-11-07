using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KUI.Windows
{
    public class FlatFrame : WindowBase
    {
        private const int AW_HOR_POSITIVE = 0X1;
        private const int AW_HOR_NEGATIVE = 0X2;
        private const int AW_VER_POSITIVE = 0X4;
        private const int AW_VER_NEGATIVE = 0X8;
        private const int AW_CENTER = 0X10;
        private const int AW_HIDE = 0X10000;
        private const int AW_ACTIVATE = 0X20000;
        private const int AW_SLIDE = 0X40000;
        private const int AW_BLEND = 0X80000;

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern int AnimateWindow(IntPtr hwand, int dwTime, int dwFlags);

        public void Present(WindowBase parent)
        {
            Present(parent, Dock);
        }

        public void Present(WindowBase parent, DockStyle dock)
        {
            Dock = dock;
            Attach(parent);
        }

        public void Attach(WindowBase parent)
        {
            ResizeRedraw = true;
            TopLevel = false;
            parent.Controls.Add(this);
            Parent = parent;
            PerformLayout();

            if (Dock == DockStyle.Top)
            {
                Size = new Size(parent.ClientRectangle.Width - parent.Padding.Top * 2, Height);
                Location = new Point(parent.Padding.Left, parent.Padding.Top);
                AnimateWindow(Handle, 200, AW_VER_POSITIVE);
            }
            else if (Dock == DockStyle.Bottom)
            {
                Size = new Size(parent.ClientRectangle.Width - parent.Padding.Left * 2, Height);
                Location = new Point(parent.Padding.Left, parent.ClientRectangle.Height - Height - parent.Padding.Bottom);
                AnimateWindow(Handle, 200, AW_VER_NEGATIVE);
            }
            else if (Dock == DockStyle.Left)
            {
                Size = new Size(Width, parent.ClientRectangle.Height - parent.Padding.Top - parent.Padding.Bottom);
                Location = new Point(parent.Padding.Left, parent.Padding.Top);
                AnimateWindow(Handle, 200, AW_HOR_POSITIVE);
            }
            else if (Dock == DockStyle.Right)
            {
                Size = new Size(Width, parent.ClientRectangle.Height - parent.Padding.Top - parent.Padding.Bottom);
                Location = new Point(parent.ClientRectangle.Width - Width, parent.Padding.Top);
                AnimateWindow(Handle, 200, AW_HOR_NEGATIVE);
            }
            else if (Dock == DockStyle.Fill)
            {
                Size = new Size(parent.ClientRectangle.Width - parent.Padding.Left * 2, parent.ClientRectangle.Height - parent.Padding.Top - parent.Padding.Bottom);
                Location = new Point(parent.Padding.Left, parent.Padding.Top);
                AnimateWindow(Handle, 200, AW_VER_POSITIVE);
            }

            (this as Control).Show();
            (this as Control).BringToFront();
            (this as Control).Parent.Invalidate(ShadeRect(Theme.ShadowSize), false);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (Parent != null)
                Parent.Invalidate(ShadeRect(Theme.ShadowSize), false);
        }
    }
}