using KUI.Controls;
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
    public class FlatWindow : WindowBase
    {
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private const int CS_DROPSHADOW = 0x20000;
        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;
        private const int wmNcHitTest = 0x84;
        private const int htLeft = 10;
        private const int htRight = 11;
        private const int htTop = 12;
        private const int htTopLeft = 13;
        private const int htTopRight = 14;
        private const int htBottom = 15;
        private const int htBottomLeft = 16;
        private const int htBottomRight = 17;

        public FlatCancelButton ExitButton = new FlatCancelButton();

        public FlatWindow()
        {
            Padding = new Padding(2);
            
            ExitButton.Size = new Size(30, 29);
            ExitButton.Text = "╳";
            ExitButton.Anchor = AnchorStyles.Top | AnchorStyles.Right;
            ExitButton.Click += (os, ea) => Close();
            Controls.Add(ExitButton);
        }

        public override Rectangle ShadeRect(int index)
        {
            return new Rectangle(1 - index, 1 - index, Width - 2 + index * 2, 30 + index * 2);
        }

        protected override CreateParams CreateParams
        {
            get
            {
                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_DROPSHADOW;
                return cp;
            }
        }

        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            //ExitButton.Location = new Point(Width - 32, 2);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == wmNcHitTest && WindowState != FormWindowState.Maximized)
            {
                int x = (int)(m.LParam.ToInt64() & 0xFFFF);
                int y = (int)((m.LParam.ToInt64() & 0xFFFF0000) >> 16);
                Point pt = PointToClient(new Point(x, y));
                Size clientSize = ClientSize;
                ///allow resize on the lower right corner
                if (pt.X >= clientSize.Width - 16 && pt.Y >= clientSize.Height - 16 && clientSize.Height >= 16)
                {
                    m.Result = (IntPtr)(IsMirrored ? htBottomLeft : htBottomRight);
                    return;
                }
                ///allow resize on the lower left corner
                if (pt.X <= 16 && pt.Y >= clientSize.Height - 16 && clientSize.Height >= 16)
                {
                    m.Result = (IntPtr)(IsMirrored ? htBottomRight : htBottomLeft);
                    return;
                }
                ///allow resize on the upper right corner
                if (pt.X <= 16 && pt.Y <= 16 && clientSize.Height >= 16)
                {
                    m.Result = (IntPtr)(IsMirrored ? htTopRight : htTopLeft);
                    return;
                }
                ///allow resize on the upper left corner
                if (pt.X >= clientSize.Width - 16 && pt.Y <= 16 && clientSize.Height >= 16)
                {
                    m.Result = (IntPtr)(IsMirrored ? htTopLeft : htTopRight);
                    return;
                }
                ///allow resize on the top border
                if (pt.Y <= 2 && clientSize.Height >= 2)
                {
                    m.Result = (IntPtr)(htTop);
                    return;
                }
                ///allow resize on the bottom border
                if (pt.Y >= clientSize.Height - 16 && clientSize.Height >= 16)
                {
                    m.Result = (IntPtr)(htBottom);
                    return;
                }
                ///allow resize on the left border
                if (pt.X <= 16 && clientSize.Height >= 16)
                {
                    m.Result = (IntPtr)(htLeft);
                    return;
                }
                ///allow resize on the right border
                if (pt.X >= clientSize.Width - 16 && clientSize.Height >= 16)
                {
                    m.Result = (IntPtr)(htRight);
                    return;
                }
            }
            base.WndProc(ref m);
        }

        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Theme.ForeBrush, 1, 2, Width - 33, 30);
            e.Graphics.DrawString(Text, Theme.TitleFont, Theme.FontBrush, 4, 5);
            DrawShadow(e.Graphics);

            base.OnPaint(e);

            if (WindowState != FormWindowState.Maximized)
                ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Theme.AccentColor, ButtonBorderStyle.Solid);
        }
    }
}
