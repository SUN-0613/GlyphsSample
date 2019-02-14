using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp3
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();

            // meiryo bbb
            //var lineSpacing = new FontFamily("Candarab").LineSpacing * bbb.FontRenderingEmSize;

        }

        private void Canvas_MouseDown(object sender, MouseButtonEventArgs e)
        {

            //bbb.SetValue(Canvas.TopProperty, (double)aaa.GetValue(Canvas.TopProperty) + aaa.RenderSize.Height);// (new FontFamily("Meiryo").LineSpacing * aaa.FontRenderingEmSize));
            //ccc.SetValue(Canvas.TopProperty, (double)bbb.GetValue(Canvas.TopProperty) + bbb.RenderSize.Height);

            double top = 0.0;
            double left = 400.0;

            for (int iLoop = 0; iLoop < 2; iLoop++)
            {

                Glyphs glyphs;
                Canvas canvas;

                switch (iLoop)
                {
                    case 0:
                        glyphs = aaa;
                        canvas = CanvasA;
                        break;

                    case 1:
                        glyphs = bbb;
                        canvas = CanvasB;
                        break;

                    default:
                        return;
                }

                /* 没ネタ System.Drawingによる計算
                var fontFamily = new System.Drawing.FontFamily(System.IO.Path.GetFileNameWithoutExtension(glyphs.FontUri.LocalPath));
                var fontStyle = glyphs.StyleSimulations.Equals(StyleSimulations.ItalicSimulation) ? System.Drawing.FontStyle.Italic : glyphs.StyleSimulations.Equals(StyleSimulations.BoldSimulation) ? System.Drawing.FontStyle.Bold : System.Drawing.FontStyle.Regular;
                var fontSize = (double)glyphs.FontRenderingEmSize;

                var emHeight = (double)fontFamily.GetEmHeight(fontStyle);
                var cellAscent = (double)(fontFamily.GetCellAscent(fontStyle));
                var cellDescent = (double)(fontFamily.GetCellDescent(fontStyle));
                var lineSpacing = (double)(fontFamily.GetLineSpacing(fontStyle));
                var pixelHeight = (fontSize.Equals(0.0) || emHeight.Equals(0.0)) ? 1.0 : (fontSize / emHeight) * 0.8;

                canvas.SetValue(Canvas.TopProperty, top);
                canvas.SetValue(Canvas.LeftProperty, 0.0);
                canvas.Height = (cellAscent + cellDescent) * pixelHeight;

                glyphs.SetValue(Canvas.TopProperty, top);
                glyphs.OriginY = cellAscent * pixelHeight;

                top = (cellAscent + cellDescent) * pixelHeight;
                */

                /* 採用 横書
                GlyphTypeface typeface = new GlyphTypeface(glyphs.FontUri, glyphs.StyleSimulations);
                double fontSize = glyphs.FontRenderingEmSize;

                canvas.SetValue(Canvas.TopProperty, top);
                canvas.SetValue(Canvas.LeftProperty, 0.0);
                canvas.Height = typeface.Height * 0.9 * fontSize;

                glyphs.SetValue(Canvas.TopProperty, top);
                glyphs.OriginY = (typeface.Height * 0.9) * fontSize;

                top += canvas.Height;
                */

                // 縦書
                GlyphTypeface typeface = new GlyphTypeface(glyphs.FontUri, glyphs.StyleSimulations);
                double fontSize = glyphs.FontRenderingEmSize;
                string message = glyphs.UnicodeString;
                double maxWidth = 0.0;

                for (int jLoop = 0; jLoop < message.Length; jLoop++)
                {

                    ushort index = typeface.CharacterToGlyphMap[message[jLoop]];
                    double width = typeface.AdvanceWidths[index] * fontSize;

                    if (maxWidth < width)
                    {
                        maxWidth = width;
                    }

                }

                canvas.SetValue(Canvas.TopProperty, 0.0);
                canvas.SetValue(Canvas.LeftProperty, left - maxWidth);
                canvas.Width = maxWidth;

                glyphs.SetValue(Canvas.LeftProperty, left);
                glyphs.OriginY = maxWidth / 2.0;

                left -= canvas.Width;

            }

        }

    }
}
