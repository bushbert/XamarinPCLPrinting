using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using Windows.Graphics.Imaging;
using Windows.Graphics.Printing;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Printing;

namespace PCLPrintExample.UWP
{
    public class Print_UWP
    {
        PrintManager printmgr = PrintManager.GetForCurrentView();
        PrintDocument PrintDoc = null;
        PrintDocument printDoc;
        PrintTask Task = null;
        Windows.UI.Xaml.Controls.Image ViewToPrint = new Windows.UI.Xaml.Controls.Image();
        public Print_UWP()
        {
            printmgr.PrintTaskRequested += Printmgr_PrintTaskRequested;
        }

        


        public async void PrintUWpAsync(byte[] imageData)
        {

            BitmapImage biSource = new BitmapImage();
            using (InMemoryRandomAccessStream stream = new InMemoryRandomAccessStream())
            {
                await stream.WriteAsync(imageData.AsBuffer());
                stream.Seek(0);
                await biSource.SetSourceAsync(stream);
            }

            ViewToPrint.Source = biSource;
            if (PrintDoc != null)
            {
                printDoc.GetPreviewPage -= PrintDoc_GetPreviewPage;
                printDoc.Paginate -= PrintDoc_Paginate;
                printDoc.AddPages -= PrintDoc_AddPages;
            }
            this.printDoc = new PrintDocument();
            try
            {
                printDoc.GetPreviewPage += PrintDoc_GetPreviewPage;
                printDoc.Paginate += PrintDoc_Paginate;
                printDoc.AddPages += PrintDoc_AddPages;

                bool showprint = await PrintManager.ShowPrintUIAsync();

            }
            catch (Exception e)
            {
                Debug.WriteLine(e.ToString());
            }
            PrintDoc = null;
            GC.Collect();

        }


        private void Printmgr_PrintTaskRequested(PrintManager sender, PrintTaskRequestedEventArgs args)
        {
            var deff = args.Request.GetDeferral();
            Task = args.Request.CreatePrintTask("Invoice", OnPrintTaskSourceRequested);

            deff.Complete();

        }
        async void OnPrintTaskSourceRequested(PrintTaskSourceRequestedArgs args)
        {
            var def = args.GetDeferral();
            await Windows.ApplicationModel.Core.CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(Windows.UI.Core.CoreDispatcherPriority.Normal, () =>
            {
                args.SetSource(printDoc.DocumentSource);
            });
            def.Complete();
        }

        private void PrintDoc_AddPages(object sender, AddPagesEventArgs e)
        {
            printDoc.AddPage(ViewToPrint);
            printDoc.AddPagesComplete();
        }

        private void PrintDoc_Paginate(object sender, PaginateEventArgs e)
        {
            PrintTaskOptions opt = Task.Options;
            printDoc.SetPreviewPageCount(1, PreviewPageCountType.Final);
        }

        private void PrintDoc_GetPreviewPage(object sender, GetPreviewPageEventArgs e)
        {
            printDoc.SetPreviewPage(e.PageNumber, ViewToPrint);
        }
    }
}
