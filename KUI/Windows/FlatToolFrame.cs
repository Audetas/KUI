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
    public class FlatToolFrame : FlatFrame
    {
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);
        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        private const int WM_NCLBUTTONDOWN = 0xA1;
        private const int HT_CAPTION = 0x2;

        public FlatToolFrame()
        {
            Padding = new Padding(10, 40, 10, 45);
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button == MouseButtons.Left && Parent != null && !(Parent is FlatToolFrame))
            {
                ReleaseCapture();
                SendMessage(Parent.Handle, WM_NCLBUTTONDOWN, HT_CAPTION, 0);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Theme.AccentBrush, 0, 0, Width, 30);
            e.Graphics.DrawString(Text, Theme.HeaderFont, Theme.FontBrush, 4, 6);
            e.Graphics.FillRectangle(Theme.ForeBrush, 0, Height - 34, Width, 34);
            DrawShadow(e.Graphics, new Rectangle(0, 0, Width - 1, 30));
            DrawShadow(e.Graphics, new Rectangle(0, Height - 34, Width, 34));
            //DrawShadow(e.Graphics, new Rectangle(Width, 29 + Theme.ShadowSize, 1, Height));

            base.OnPaint(e);
        }

        private Rectangle ShadeRect(Rectangle origin, int index)
        {
            return new Rectangle(origin.X - index, origin.Y - index, origin.Width + index * 2, origin.Height + index * 2);
        }

        private void DrawShadow(Graphics g, Rectangle rect)
        {
            for (int i = 0; i < Theme.ShadowSize; i++)
            {
                g.DrawRectangle(
                    new Pen(Theme.ShadowColor.Shade(Theme.ShadowSize, i)),
                    ShadeRect(rect, i));
            }
        }
    }
}