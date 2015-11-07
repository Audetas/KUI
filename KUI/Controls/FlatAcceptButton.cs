using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KUI.Controls
{
    public class FlatAcceptButton : FlatButton
    {
        protected override void OnPaintBackground(PaintEventArgs pevent)
        {
            pevent.Graphics.FillRectangle(
                MouseOver ? Brushes.LimeGreen : Brushes.LimeGreen, ClientRectangle);
        }

        /*
        public override void DrawShadow(Graphics g)
        {
            if (HasShadow)
            {
                for (int i = 0; i < ShadowLevel; i++)
                {
                    g.DrawRectangle(
                        new Pen(Color.LimeGreen.Shade(Theme.ShadowSize, i)),
                        ShadeRect(i));
                }
            }
        }*/
    }
}
