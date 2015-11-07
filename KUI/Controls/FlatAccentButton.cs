using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KUI.Controls
{
    public class FlatAccentButton : FlatButton
    {
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            pevent.Graphics.FillRectangle(
                MouseOver ? Theme.ForeBrush : Theme.AccentBrush, ClientRectangle);
        }

        public override void DrawShadow(Graphics g)
        {
            if (HasShadow)
            {
                for (int i = 0; i < ShadowLevel; i++)
                {
                    g.DrawRectangle(
                        new Pen(Theme.AccentColor.Shade(Theme.ShadowSize, i)),
                        ShadeRect(i));
                }
            }
        }
    }
}
