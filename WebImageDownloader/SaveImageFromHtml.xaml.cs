using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace WebImageDownloader
{
    /// <summary>
    /// Interaction logic for SaveImageFromHtml.xaml
    /// </summary>
    public partial class SaveImageFromHtml : Window
    {
        public SaveImageFromHtml()
        {
            InitializeComponent();
        }

        public static int process, all;
        private string sourcepath = "";
        private string savepath = "";
        private string savename = "";
        private int filesize;

        private void button1_Click(object sender, RoutedEventArgs e)
        {
            sourcepath = textBoxOpen.Text;
            savepath = textBoxSaveTo.Text;
            savename = textBoxFileName.Text;
            filesize = Int32.Parse(textBoxFileSize.Text);
            Thread tr = new Thread(new ThreadStart(Process));
            tr.Start();
            this.Dispatcher.Invoke(
                System.Windows.Threading.DispatcherPriority.Normal,
                    new Action(
                        delegate()
                        {
                            UpdateProgress(process, all);
                        }
                        ));
        }

        private void Process()
        {
            if (sourcepath == "")
            {
                sourcepath = System.IO.Directory.GetCurrentDirectory();
            }

            //bien cho processbar

            //lay tat ca cac file htm trong thu muc
            string source = sourcepath;
            string[] all_files1 = System.IO.Directory.GetFiles(source, "*.htm");
            string[] all_files2 = System.IO.Directory.GetFiles(source, "*.php");
            //string[] all_files3 = System.IO.Directory.GetFileSystemEntries(source, "*.html");

            //ghep 3 mang lam mot
            string[] all_files = new string[all_files1.Length + all_files2.Length];
            int count1 = all_files1.Length;
            int count2 = all_files2.Length;

            for (int i = 0; i < all_files1.Length; i++)
            {
                all_files[i] = all_files1[i];
            }

            for (int j = 0; j < all_files2.Length; j++)
            {
                all_files[j + count1] = all_files2[j];
            }

            all = all_files.Length;
            //thuc hien save anh
            foreach (string file in all_files)
            {
                string createsource = file.Substring(0, file.LastIndexOf("."));//tru di 4 ki tu .htm
                createsource = createsource.Substring(createsource.LastIndexOf("\\") + 1);//lay phan cuoi cung trong file lam ten thu muc
                string createpath = savepath + "\\" + createsource; ;//ghep ten thu muc va duong dan moi de tao thu muc moi
                System.IO.Directory.CreateDirectory(createpath);

                string htmlURL = file;
                string htmString = System.IO.File.ReadAllText(@htmlURL);

                //lay ve cac tag img trong html string
                string pattern = @"<(img)\b[^>]*>";
                Regex rgx = new Regex(pattern, RegexOptions.IgnoreCase);
                MatchCollection matches = rgx.Matches(htmString);
                //tach lay cai link image va down ve
                for (int i = 0, l = matches.Count; i < l; i++)
                {

                    //cat lay cac link source cua cac image
                    string temp = matches[i].Value;
                    temp = temp.Substring(temp.LastIndexOf("src=") + 5);
                    if (temp.IndexOf("\"") >= 0)
                    {
                        temp = temp.Substring(0, temp.IndexOf("\""));
                    }
                    temp = temp.Substring(temp.LastIndexOf("/") + 1);
                    temp = htmlURL.Substring(0, file.LastIndexOf(".")) + "_files" + "\\" + temp;
                    string directory = createpath + "\\" + savename + i + ".jpg";
                    try
                    {
                        System.Drawing.Image objimage = System.Drawing.Image.FromFile(@temp);
                        if ((objimage.Height >= filesize) && (objimage.Width >= filesize))
                        {
                            File.Copy(@temp, @directory);
                        }
                    }
                    catch (Exception ex)
                    {
                        //do nothing
                    }
                }

                process++;
                //update process bar

            }

            System.Windows.MessageBox.Show("Done!");
        }

        private void buttonBrowser_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fl = new FolderBrowserDialog();
            fl.SelectedPath = System.IO.Directory.GetCurrentDirectory();
            fl.ShowNewFolderButton = true;
            if (fl.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBoxOpen.Text = fl.SelectedPath;
            }
        }

        private void UpdateProgress(int Process, int all)
        {
            try
            {
                // Calculate the download progress in percentages
                float PercentProgress = (Process / all) * 100;
                // Make progress on the progress bar
                progressBar1.Value = PercentProgress;
            }
            catch (Exception ex)
            {
            }
        }
    }
}
