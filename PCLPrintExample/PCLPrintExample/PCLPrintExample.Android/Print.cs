using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.Graphics;
using Android.OS;
using Android.Print;
using Android.Runtime;
using Android.Support.V4.Print;
using Android.Views;
using Android.Widget;

namespace PCLPrintExample.Droid
{
    public class Print : IPrint
    {
        void IPrint.Print(byte[] content)
        {
            //Android print code goes here

        }
    }
}