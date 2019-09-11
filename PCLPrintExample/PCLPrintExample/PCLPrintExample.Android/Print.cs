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
            Stream inputStream = new MemoryStream(formData);
            string fileName = "form.pdf";
            if (inputStream.CanSeek)
                //Reset the position of PDF document stream to be printed
                inputStream.Position = 0;
            //Create a new file in the Personal folder with the given name
            string createdFilePath = System.IO.Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal), fileName);
            //Save the stream to the created file
            using (var dest = System.IO.File.OpenWrite(createdFilePath))
                inputStream.CopyTo(dest);
            string filePath = createdFilePath;
            PrintManager printManager = (PrintManager)Forms.Context.GetSystemService(Context.PrintService);
            PrintDocumentAdapter pda = new CustomPrintDocumentAdapter(filePath);
            //Print with null PrintAttributes
            printManager.Print(fileName, pda, null);
        }
    }
}
