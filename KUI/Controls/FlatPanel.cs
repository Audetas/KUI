using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KUI.Controls
{
    [Designer("System.Windows.Forms.Design.ParentControlDesigner, System.Design", typeof(IDesigner))]
    public class FlatPanel : ControlBase
    {
        public bool ShowText = true;

        public FlatPanel()
        {
            DoubleBuffered = true;
            Padding = new Padding(5);
        }

        public override void DrawShadow(Graphics g)
        {
            for (int i = 0; i < Theme.ShadowSize; i++)
            {
                g.DrawRectangle(
                    new Pen(Theme.ShadowColor.Shade(Theme.ShadowSize, i)),
                    ShadeRect(i));
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.FillRectangle(Theme.ForeBrush, ClientRectangle);

            if (ShowText)
                e.Graphics.DrawString(Text, Theme.TitleFont, Theme.FontBrush, 2, 2);

            foreach (Control c in Controls)
                if (c is ControlBase)
                    (c as ControlBase).DrawShadow(e.Graphics);

            base.OnPaint(e);
        }

        protected override void OnMouseEnter(EventArgs e)
        {
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            //return;
            foreach (Control c in Controls)
                if (c is ControlBase && (c as ControlBase).ShadowLevel != 0)
                    (c as ControlBase).Invalidate();
        }
    }
}
