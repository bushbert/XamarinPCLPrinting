using PCLPrintExample.UWP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;
using Xamarin.Forms;

[assembly: Dependency(typeof(Print))]

namespace PCLPrintExample.UWP
{
    public class Print : IPrint
    {
        void IPrint.Print(byte[] content)
        {
            Print_UWP printing = new Print_UWP();
            printing.PrintUWpAsync(content);
        }
    }
}
