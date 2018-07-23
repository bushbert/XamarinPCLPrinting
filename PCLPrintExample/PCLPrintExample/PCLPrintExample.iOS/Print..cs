using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CoreGraphics;
using Foundation;
using PCLPrintExample.iOS;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(PrintHelper))]
namespace PCLPrintExample.iOS
{
    public class PrintHelper : IPrint
    {

        void IPrint.Print(byte[] content)
        {

            var data = NSData.FromArray(content);
            var uiimage = UIImage.LoadFromData(data);

            var printer = UIPrintInteractionController.SharedPrintController;



            if (printer == null)
            {

                Console.WriteLine("Unable to print at this time.");

            }
            else
            {

                var printInfo = UIPrintInfo.PrintInfo;

                printInfo.OutputType = UIPrintInfoOutputType.General;

                printInfo.JobName = "Print Job Name";



                printer.PrintInfo = printInfo;

                printer.PrintingItem = uiimage;

                printer.ShowsPageRange = true;



                var handler = new UIPrintInteractionCompletionHandler((printInteractionController, completed, error) =>
                {

                    if (completed)
                    {

                        Console.WriteLine("Print Completed.");

                    }
                    else if (!completed && error != null)
                    {

                        Console.WriteLine("Error Printing.");

                    }

                });

                CGRect frame = new CGRect();
                frame.Size = uiimage.Size;



                    printer.Present(true, handler);

            }
        }
    }
}