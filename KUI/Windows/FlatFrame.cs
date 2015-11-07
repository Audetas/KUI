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

        public FlatFrame Present(WindowBase parent)
        {
            Present(parent, Dock);
            return this;
        }

        public FlatFrame Present(WindowBase parent, DockStyle dock)
        {
            Dock = dock;
            Attach(parent);
            return this;
        }

        public void PresentReplacement(WindowBase replacee, DockStyle dock)
        {
            replacee.Hide();
            Dock = dock;
            Attach(replacee.Parent as WindowBase);
            FormClosed += (s, e) =>
            {
                replacee.Show();
            };
        }

        public void OnExit(Action a)
        {
            FormClosed += (s, e) => a();
        }

        public void Attach(WindowBase parent)
        {
            ResizeRedraw = true;
            TopLevel = false;
            parent.Controls.Add(this);
            Parent = parent;
            PerformLayout();
            //Animate();

            (this as Control).Show();
            (this as Control).BringToFront();
            (this as Control).Parent.Invalidate(ShadeRect(Theme.ShadowSize), false);
        }

        public void Hide()
        {
            base.Hide();

            if (Parent != null)
                Parent.Invalidate(ShadeRect(Theme.ShadowSize), false);
        }

        public void Show()
        {
            //Animate();
            base.Show();

            if (Parent != null)
                Parent.Invalidate(ShadeRect(Theme.ShadowSize), false);
        }

        private void Animate()
        {
            return;
            if (Dock == DockStyle.Top)
            {
                Size = new Size(Parent.ClientRectangle.Width - Parent.Padding.Top * 2, Height);
                Location = new Point(Parent.Padding.Left, Parent.Padding.Top);
                AnimateWindow(Handle, 200, AW_VER_POSITIVE);
            }
            else if (Dock == DockStyle.Bottom)
            {
                Size = new Size(Parent.ClientRectangle.Width - Parent.Padding.Left * 2, Height);
                Location = new Point(Parent.Padding.Left, Parent.ClientRectangle.Height - Height - Parent.Padding.Bottom);
                AnimateWindow(Handle, 200, AW_VER_NEGATIVE);
            }
            else if (Dock == DockStyle.Left)
            {
                Size = new Size(Width, Parent.ClientRectangle.Height - Parent.Padding.Top - Parent.Padding.Bottom);
                Location = new Point(Parent.Padding.Left, Parent.Padding.Top);
                AnimateWindow(Handle, 200, AW_HOR_POSITIVE);
            }
            else if (Dock == DockStyle.Right)
            {
                Size = new Size(Width, Parent.ClientRectangle.Height - Parent.Padding.Top - Parent.Padding.Bottom);
                Location = new Point(Parent.ClientRectangle.Width - Width, Parent.Padding.Top);
                AnimateWindow(Handle, 200, AW_HOR_NEGATIVE);
            }
            else if (Dock == DockStyle.Fill)
            {
                Size = new Size(Parent.ClientRectangle.Width - Parent.Padding.Left * 2, Parent.ClientRectangle.Height - Parent.Padding.Top - Parent.Padding.Bottom);
                Location = new Point(Parent.Padding.Left, Parent.Padding.Top);
                AnimateWindow(Handle, 200, AW_HOR_POSITIVE);
            }
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);

            if (Parent != null)
                Parent.Invalidate(ShadeRect(Theme.ShadowSize), false);
        }

        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            base.OnFormClosed(e);

            if (Parent != null)
                Parent.Invalidate(ShadeRect(Theme.ShadowSize), false);
        }
    }
}