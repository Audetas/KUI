using KUI.Windows;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KUI
{ 
    public static class Theme
    {
        public static Font TitleFont = new Font("Segoe UI", 12);
        public static Font HeaderFont = new Font("Segoe UI", 9, FontStyle.Bold);
        public static Font BodyFont = new Font("Segoe UI", 9);

        public static Color FontColor = Color.White;
        public static SolidBrush FontBrush = new SolidBrush(FontColor);
        public static Pen FontPen = new Pen(FontColor);

        public static Color ForeColor = Color.FromArgb(63, 63, 70);
        public static SolidBrush ForeBrush = new SolidBrush(ForeColor);
        public static Pen ForePen = new Pen(ForeColor);

        public static Color BackColor = Color.FromArgb(45, 45, 48);
        public static SolidBrush BackBrush = new SolidBrush(BackColor);
        public static Pen BackPen = new Pen(BackColor);

        public static Color AccentColor = Color.DodgerBlue;
        public static SolidBrush AccentBrush = new SolidBrush(AccentColor);
        public static Pen AccentPen = new Pen(AccentColor);

        public static int ShadowSize = 8;
        public static Color ShadowColor = Color.FromArgb(30, 30, 30);

        public static void SetFont(string fontName, int bodySize, int titleSize)
        {
            TitleFont = new Font(fontName, titleSize);
            HeaderFont = new Font(fontName, bodySize, FontStyle.Bold);
            BodyFont = new Font(fontName, bodySize);
        }

        public static void SetFontColor(Color c)
        {
            FontColor = c;
            FontBrush = new SolidBrush(c);
            FontPen = new Pen(c);
        }

        public static void SetForeColor(Color c)
        {
            ForeColor = c;
            ForeBrush = new SolidBrush(c);
            ForePen = new Pen(c);
        }

        public static void SetBackColor(Color c)
        {
            BackColor = c;
            BackBrush = new SolidBrush(c);
            BackPen = new Pen(c);
        }

        public static void SetAccentColor(Color c)
        {
            AccentColor = c;
            AccentBrush = new SolidBrush(c);
            AccentPen = new Pen(c);
        }

        public static void SetShadowSize(int size)
        {
            ShadowSize = size;
        }

        public static void SetShadowColor(Color c)
        {
            ShadowColor = c;
        }

        public static void RandomizeAuto(Action callback)
        {
            WebBrowser w = new WebBrowser();
            w.Navigate("http://kronks.me/colorscheme.html");
            w.DocumentCompleted += (s, e) =>
            {
                SetFontColor(ColorTranslator.FromHtml(w.Document.Title.Between("font", "|")));
                SetForeColor(ColorTranslator.FromHtml(w.Document.Title.Between("fore", "|")));
                SetBackColor(ColorTranslator.FromHtml(w.Document.Title.Between("extra", "|")));
                SetAccentColor(ColorTranslator.FromHtml(w.Document.Title.Between("accent", "|")));

                w.Dispose();
                callback();
            };
        }


        public static void Randomize()
        {
            System.Diagnostics.Process.Start("https://coolors.co/app/");
            string result = Microsoft.VisualBasic.Interaction.InputBox(
                "Input a Coolor set link:", "Import Color Scheme", "https://coolors.co/app");

            if (result.Length > 40)
            {
                string colorString = result.Split(new string[] { "https://coolors.co/app/" }, StringSplitOptions.None)[1];
                Color[] set = new Color[5];

                int index = 0;
                foreach (string color in colorString.Split('-'))
                {
                    set[index] = ColorTranslator.FromHtml('#' + color);
                    index++;
                }
                SetFontColor(set[0]);
                SetForeColor(set[1]);
                SetBackColor(set[2]);
                SetAccentColor(set[3]);
            }
        }
    }
}
