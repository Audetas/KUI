using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KUI.Controls
{
    public class FlatForeCheckBox : FlatCheckBox
    {
        public override void DrawShadow(Graphics g)
        {
            if (MouseOver)
            {
                for (int i = 0; i < ShadowLevel; i++)
                {
                    g.DrawRectangle(
                        new Pen(Theme.BackColor.Shade(Theme.ShadowSize, i)),
                        ShadeRect(i));
                }
            }
        }

        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            pevent.Graphics.FillRectangle(Theme.ForeBrush, ClientRectangle);

            if (MouseOver)
                pevent.Graphics.FillRectangle(
                    new SolidBrush(Theme.BackColor.Shade(Theme.ShadowSize, 0)), ClientRectangle);
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.DrawRectangle(Theme.FontPen, 1, 1, Height - 2, Height - 2);

            if (Checked)
                e.Graphics.FillRectangle(Theme.FontBrush, 4, 4, Height - 7, Height - 7);

            SizeF textSize = e.Graphics.MeasureString(Text, Theme.BodyFont);
            e.Graphics.DrawString(Text, Theme.BodyFont, Theme.FontBrush, 
                Height + 3, Height / 2 - textSize.Height / 2);
        }
    }
}
