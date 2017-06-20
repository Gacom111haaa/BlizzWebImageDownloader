using System;
using System.Collections.Generic;
using System.IO;
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
using System.Windows.Shapes;

namespace WebImageDownloader
{
    /// <summary>
    /// Interaction logic for AddItem.xaml
    /// </summary>
    public partial class AddItem : Window
    {

        WID main = null;

        public AddItem()
        {
            InitializeComponent();
        }

        public AddItem(WID _main)
        {
            main = _main;
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (!textBox_URL.Text.Equals(""))
            {
                string Address = textBox_URL.Text.Trim();
                //main.AddReturn(URL);

                string string1 = main.SaveLink;
                string string2 = main.SaveName;
                string string3 = Address;

                Running.IsIndeterminate = true;

                if (general_check.IsChecked == true)
                {
                    Task taskCreateList =
                           Task.Factory.StartNew(() =>
                           {
                               GenaralDownload GD = new GenaralDownload(string1, string2, string3);
                               GD.GetImagesLinkFromUrl2();

                           }).ContinueWith(ant =>
                           {
                               Running.IsIndeterminate = false;
                               main.AddReturn();
                               this.Close();
                           }, TaskScheduler.FromCurrentSynchronizationContext());
                }

                else if (string3.IndexOf("manga24h.com") > 0) // manag24h
                {
                    Task taskCreateList =
                       Task.Factory.StartNew(() =>
                       {
                           Manga24Download GD = new Manga24Download(string1, string2, string3);
                           GD.GetImagesLinkFromUrl2();

                       }).ContinueWith(ant =>
                       {
                           Running.IsIndeterminate = false;
                           main.AddReturn();
                           this.Close();
                       }, TaskScheduler.FromCurrentSynchronizationContext());
                }

                else if (Address.IndexOf("truyentranhtuan") > 0) // truyentranhtuan
                {
                    Task taskCreateList =
                       Task.Factory.StartNew(() =>
                       {
                           TruyentrantuanDownload GD = new TruyentrantuanDownload(string1, string2, string3);
                           GD.GetImagesLinkFromUrl2();

                       }).ContinueWith(ant =>
                       {
                           Running.IsIndeterminate = false;
                           main.AddReturn();
                           this.Close();
                       }, TaskScheduler.FromCurrentSynchronizationContext());
                }

                else if (Address.IndexOf("bcfakes.com") > 0) // bcfake
                {
                    Task taskCreateList =
                       Task.Factory.StartNew(() =>
                       {
                           BCFDownload GD = new BCFDownload(string1, string2, string3);
                           GD.GetImagesLinkFromUrl2();

                       }).ContinueWith(ant =>
                       {
                           Running.IsIndeterminate = false;
                           main.AddReturn();
                           this.Close();
                       }, TaskScheduler.FromCurrentSynchronizationContext());
                }

                else if (Address.IndexOf("urlgalleries") > 0) // urlgalleries
                {
                    Task taskCreateList =
                       Task.Factory.StartNew(() =>
                       {
                           urlgalleriesDownload GD = new urlgalleriesDownload(string1, string2, string3);
                           GD.GetImagesLinkFromUrl2();

                       }).ContinueWith(ant =>
                       {
                           Running.IsIndeterminate = false;
                           main.AddReturn();
                           this.Close();
                       }, TaskScheduler.FromCurrentSynchronizationContext());
                }

                else if (Address.IndexOf("fuskator.com") > 0) // urlgalleries
                {

                    Task taskCreateList =
                       Task.Factory.StartNew(() =>
                       {
                           fuskator GD = new fuskator(string1, string2, string3);
                           GD.GetImagesLinkFromUrl2();

                       }).ContinueWith(ant =>
                       {
                           Running.IsIndeterminate = false;
                           main.AddReturn();
                           this.Close();
                       }, TaskScheduler.FromCurrentSynchronizationContext());
                }

                else if (Address.IndexOf("imagefap.com") > 0) // urlgalleries
                {

                    Task taskCreateList =
                       Task.Factory.StartNew(() =>
                       {
                           imagefap GD = new imagefap(string1, string2, string3);
                           GD.GetImagesLinkFromUrl2();

                       }).ContinueWith(ant =>
                       {
                           Running.IsIndeterminate = false;
                           main.AddReturn();
                           this.Close();
                       }, TaskScheduler.FromCurrentSynchronizationContext());
                }

                else if (Address.IndexOf("eroticity.net") > 0) // urlgalleries
                {

                    Task taskCreateList =
                       Task.Factory.StartNew(() =>
                       {
                           eroticity GD = new eroticity(string1, string2, string3);
                           GD.GetImagesLinkFromUrl2();

                       }).ContinueWith(ant =>
                       {
                           Running.IsIndeterminate = false;
                           main.AddReturn();
                           this.Close();
                       }, TaskScheduler.FromCurrentSynchronizationContext());
                }

                else // General download
            {
                Task taskCreateList =
                   Task.Factory.StartNew(() =>
                   {
                       GenaralDownload GD = new GenaralDownload(string1, string2, string3);
                       GD.GetImagesLinkFromUrl2();

                   }).ContinueWith(ant =>
                   {
                       Running.IsIndeterminate = false;
                       main.AddReturn();
                       this.Close();
                   }, TaskScheduler.FromCurrentSynchronizationContext());
            }

                
            }
            else
            {
                MessageBox.Show("Error","URL is empty");
            }
        }
    }
}
