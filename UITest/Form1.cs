using KUI;
using KUI.Windows;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace UITest
{
    public partial class Form1 : FlatWindow
    {
        public Form1() : base()
        {
            InitializeComponent();
        }

        private void flatButton1_Click(object sender, EventArgs e)
        {
            Theme.RandomizeAuto(() => Refresh());
        }

        private void flatButton3_Click(object sender, EventArgs e)
        {
            FlatFrame ff = new FlatFrame();
            ff.Width = 150;
            ff.Present(this, DockStyle.Right);
        }

        private void flatButton4_Click(object sender, EventArgs e)
        {
            FlatToolFrame tf = new FlatToolFrame();
            tf.Width = 150;
            tf.Text = "ToolFrame1";
            tf.Present(this, DockStyle.Left);
        }
    }
}
