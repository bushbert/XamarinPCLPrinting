using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace PCLPrintExample
{
	public partial class MainPage : ContentPage
	{
        

        private byte[] imageContent;
		public MainPage()
		{
			InitializeComponent();
            DownloadPicAsync("https://vector.me/files/images/1/7/17513/tottenham_hotspur_fc.png");

        }

        private void DownloadPicAsync(string url)
        {
            using (var webClient = new WebClient())
            {
                var imageBytes = webClient.DownloadData(new Uri(url));
                if (imageBytes != null && imageBytes.Length > 0)
                {
                    PrintableContent.Source = ImageSource.FromStream(() => new MemoryStream(imageBytes));
                    imageContent = imageBytes;
                }
            }
        }

        


        private void btnPrint_Clicked(object sender, EventArgs e)
        {
            

            DependencyService.Get<IPrint>().Print(imageContent);

        }
    }
}
