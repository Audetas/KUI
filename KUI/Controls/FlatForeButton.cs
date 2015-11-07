using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KUI.Controls
{
    public class FlatForeButton : FlatButton
    {
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            pevent.Graphics.FillRectangle(
                MouseOver ? Theme.AccentBrush : Theme.BackBrush, ClientRectangle);
        }

        public override void DrawShadow(Graphics g)
        {
            if (HasShadow)
            {
                for (int i = 0; i < ShadowLevel; i++)
                {
                    g.DrawRectangle(
                        new Pen(Theme.BackColor.Shade(Theme.ShadowSize, i)),
                        ShadeRect(i));
                }
            }
        }
    }
}
