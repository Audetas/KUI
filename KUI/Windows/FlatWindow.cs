using KUI.Controls;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
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

        public FlatWindow()
        {
            //FormBorderStyle = FormBorderStyle.Sizable;
            Padding = new Padding(2, 31, 2, 2);
            ResizeRedraw = true;
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

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == wmNcHitTest && WindowState != FormWindowState.Maximized)
            {
                int gripDist = 10;
                //int x = (int)(m.LParam.ToInt64() & 0xFFFF);
                //int x = Cursor.Position.X;
               // int y = (int)((m.LParam.ToInt64() & 0xFFFF0000) >> 16);
                //Console.WriteLine(x);
                Point pt = PointToClient(Cursor.Position);
                //Console.WriteLine(pt);
                Size clientSize = ClientSize;
                ///allow resize on the lower right corner
                if (pt.X >= clientSize.Width - gripDist && pt.Y >= clientSize.Height - gripDist && clientSize.Height >= gripDist)
                {
                    m.Result = (IntPtr)(IsMirrored ? htBottomLeft : htBottomRight);
                    return;
                }
                ///allow resize on the lower left corner
                if (pt.X <= gripDist && pt.Y >= clientSize.Height - gripDist && clientSize.Height >= gripDist)
                {
                    m.Result = (IntPtr)(IsMirrored ? htBottomRight : htBottomLeft);
                    return;
                }
                ///allow resize on the upper right corner
                if (pt.X <= gripDist && pt.Y <= gripDist && clientSize.Height >= gripDist)
                {
                    m.Result = (IntPtr)(IsMirrored ? htTopRight : htTopLeft);
                    return;
                }
                ///allow resize on the upper left corner
                if (pt.X >= clientSize.Width - gripDist && pt.Y <= gripDist && clientSize.Height >= gripDist)
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
                if (pt.Y >= clientSize.Height - gripDist && clientSize.Height >= gripDist)
                {
                    m.Result = (IntPtr)(htBottom);
                    return;
                }
                ///allow resize on the left border
                if (pt.X <= gripDist && clientSize.Height >= gripDist)
                {
                    m.Result = (IntPtr)(htLeft);
                    return;
                }
                ///allow resize on the right border
                if (pt.X >= clientSize.Width - gripDist && clientSize.Height >= gripDist)
                {
                    m.Result = (IntPtr)(htRight);
                    return;
                }
            }
            base.WndProc(ref m);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);

                if (new Rectangle(Width - 31, 2, 29, 29).Contains(e.Location)) Close();
                if (new Rectangle(Width - 89, 2, 29, 29).Contains(e.Location)) WindowState = FormWindowState.Minimized;
                if (new Rectangle(Width - 60, 2, 29, 29).Contains(e.Location))
                {
                    Screen currentScreen = Screen.FromPoint(Location);
                    Rectangle workingArea = Screen.FromPoint(Location).WorkingArea;

                    if (WindowState == FormWindowState.Maximized)
                    {
                        WindowState = FormWindowState.Normal;
                        //Bounds = _lastBounds;
                    }
                    else
                    {
                        //_lastBounds = Bounds;
                        if (currentScreen == Screen.PrimaryScreen)
                            MaximizedBounds = workingArea;
                        else
                            MaximizedBounds = new Rectangle(0, 0, workingArea.Width, workingArea.Height);
                        WindowState = FormWindowState.Maximized;
                        Console.WriteLine(workingArea);
                        Console.WriteLine(Bounds);
                        //Bounds = workingArea;
                    }

                    OnResize(null);
                }
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            bool invalidate = false;

            bool temp = _wnd_exitOver;
            _wnd_exitOver = new Rectangle(Width - 31, 2, 29, 29).Contains(e.Location);
            if (temp != _wnd_exitOver) invalidate = true;

            temp = _wnd_maximOver;
            _wnd_maximOver = new Rectangle(Width - 60, 2, 29, 29).Contains(e.Location);
            if (temp != _wnd_maximOver) invalidate = true;

            temp = _wnd_minimOver;
            _wnd_minimOver = new Rectangle(Width - 89, 2, 29, 29).Contains(e.Location);
            if (temp != _wnd_minimOver) invalidate = true;

            if (invalidate) Invalidate(false);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            bool invalidate = _wnd_exitOver || _wnd_maximOver || _wnd_minimOver;
            _wnd_exitOver = _wnd_maximOver = _wnd_minimOver = false;
            if (invalidate) Invalidate(false);
        }

        private bool _wnd_exitOver = false;
        private bool _wnd_maximOver = false;
        private bool _wnd_minimOver = false;

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Theme.ForeBrush, 1, 2, Width - 3, 30);
            e.Graphics.DrawString(Text, Theme.TitleFont, Theme.FontBrush, 4, 5);
            DrawShadow(e.Graphics);
            base.OnPaint(e);

            //Exit button
            if (_wnd_exitOver) e.Graphics.FillRectangle(Brushes.IndianRed, Width - 31, 2, 29, 29);
            e.Graphics.DrawLine(Theme.FontPen, Width - 31 + 9, 2 + 9, Width - 31 + 19, 2 + 19);
            e.Graphics.DrawLine(Theme.FontPen, Width - 31 + 19, 2 + 9, Width - 31 + 9, 2 + 19);

            //Maximize button
            if (_wnd_maximOver) e.Graphics.FillRectangle(Theme.BackBrush, Width - 60, 2, 29, 29);
            e.Graphics.DrawRectangle(Theme.FontPen, Width - 60 + 9, 2 + 9, 10, 10);

            //Minimize button
            if (_wnd_minimOver) e.Graphics.FillRectangle(Theme.BackBrush, Width - 89, 2, 29, 29);
            e.Graphics.DrawLine(Theme.FontPen, Width - 89 + 9, 2 + 19, Width - 89 + 19, 2 + 19);        

            if (WindowState != FormWindowState.Maximized)
                ControlPaint.DrawBorder(e.Graphics, ClientRectangle, Theme.AccentColor, ButtonBorderStyle.Solid);
        }
    }
}
