using PCLPrintExample.WPF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using Xamarin.Forms;

[assembly: Dependency(typeof(Print))]
namespace PCLPrintExample.WPF
{
    public class Print : IPrint
    {
        void IPrint.Print(byte[] content)
        {
            PrintDialog printDialog = new PrintDialog();
            if (printDialog.ShowDialog() == true)
            {

                var bi = ToImage(content);

                var vis = new DrawingVisual();
                var dc = vis.RenderOpen();
                dc.DrawImage(bi, new Rect { Width = bi.Width, Height = bi.Height });
                dc.Close();

                var pdialog = new PrintDialog();
                if (pdialog.ShowDialog() == true)
                {
                    pdialog.PrintVisual(vis, "My Image");
                }

            }
                 }

        public BitmapImage ToImage(byte[] array)
        {
            using (var ms = new System.IO.MemoryStream(array))
            {
                var image = new BitmapImage();
                image.BeginInit();
                image.CacheOption = BitmapCacheOption.OnLoad; // here
                image.StreamSource = ms;
                image.EndInit();
                return image;
            }
        }
    }
}
