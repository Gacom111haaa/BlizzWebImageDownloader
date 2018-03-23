
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace WebImageDownloader
{
    /// <summary>
    /// Interaction logic for SaveImageFromHtml.xaml
    /// </summary>
    public partial class SaveImageFromHtml : Window
    {
        WID main = null;
        private List<ItemHtmlSelect> listhtmlFile = new List<ItemHtmlSelect>();

        public SaveImageFromHtml(WID _main)
        {
            main = _main;
            InitializeComponent();
        }

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
            foreach(object row in grvHtmlFile.Items)
            {             
                string linkSelect = ((ItemHtmlSelect)row).link;
                if (File.Exists(linkSelect))
                {
                    string Feedback = Run(linkSelect);
                    listhtmlFile.Where(x => x.link == linkSelect).FirstOrDefault().status = Feedback;
                    grvHtmlFile.DataContext = null;
                    grvHtmlFile.DataContext = listhtmlFile;
                }
                else
                {
                    //System.Windows.MessageBox.Show("File not found");
                    listhtmlFile.Where(x => x.link == linkSelect).FirstOrDefault().status = "File not found";
                    grvHtmlFile.DataContext = null;
                    grvHtmlFile.DataContext = listhtmlFile;
                }
            }
        }


        private string Run(string link)
        {
            string string1 = main.SaveLink;
            string string2 = main.SaveName;
            string string3 = link;
            GenaralDownload GD = new GenaralDownload(string1, string2, string3);
            string CheckString = GD.GetImagesLinkFromUrl5();
            main.AddReturn();
            return CheckString;
        }

        /// <summary>
        /// Hàm biểu thức chính quy, không cần sử dụng itextsharpt
        /// </summary>
        private void Process()
        {
            if (sourcepath == "")
            {
                sourcepath = Directory.GetCurrentDirectory();
            }

            //bien cho processbar

            //lay tat ca cac file htm trong thu muc
            string source = sourcepath;
            string[] all_files1 = Directory.GetFiles(source, "*.htm");
            string[] all_files2 = Directory.GetFiles(source, "*.php");
            //string[] all_files3 = System.IO.Directory.GetFileSystemEntries(source, "*.html");

            //ghep 3 mang lam mot
            string[] all_files = new string[all_files1.Length + all_files2.Length];
            int count1 = all_files1.Length;
            //int count2 = all_files2.Length;

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
                Directory.CreateDirectory(createpath);

                string htmlURL = file;
                string htmString = File.ReadAllText(@htmlURL);

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
            Microsoft.Win32.OpenFileDialog openFileDialog = new Microsoft.Win32.OpenFileDialog() { Filter = "Html files (*.html)|*.html|All files (*.*)|*.*", Multiselect = true };
            var dialogResult = openFileDialog.ShowDialog();
            if (dialogResult == true)
            {
                listhtmlFile.Clear();
                grvHtmlFile.DataContext = null;
                foreach (string file in openFileDialog.FileNames)
                {
                    ItemHtmlSelect item = new ItemHtmlSelect(file, "Uncheck");
                    listhtmlFile.Add(item);
                }
                grvHtmlFile.DataContext = listhtmlFile;
            }
        }

        private void btnMove_Click(object sender, RoutedEventArgs e)
        {
            int select = grvHtmlFile.SelectedIndex;
            string linkSelect = ((ItemHtmlSelect)grvHtmlFile.SelectedItem).link;

            if (File.Exists(linkSelect))
            {
                string path1 = linkSelect.Substring(0, linkSelect.LastIndexOf("\\"));
                string path2 = linkSelect.Substring(linkSelect.LastIndexOf("\\") +1);
                string newpath = string.Format("{0}\\Check\\{1}",path1 ,path2 );
                if(!Directory.Exists(path1 + "\\Check"))
                {
                    Directory.CreateDirectory(path1 + "\\Check");
                }
                File.Move(linkSelect, newpath);
                listhtmlFile[select].status = "Moved";
                grvHtmlFile.DataContext = null;
                grvHtmlFile.DataContext = listhtmlFile;
            }
            else
            {
                //System.Windows.MessageBox.Show("File not found");
                listhtmlFile[select].status = "File not found";
                grvHtmlFile.DataContext = null;
                grvHtmlFile.DataContext = listhtmlFile;
            }
        }

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            int select = grvHtmlFile.SelectedIndex;
            string linkSelect = ((ItemHtmlSelect)grvHtmlFile.SelectedItem).link;

            if(File.Exists(linkSelect))
            {
                FileSystem.DeleteFile(linkSelect, UIOption.OnlyErrorDialogs, RecycleOption.SendToRecycleBin);
                listhtmlFile[select].status = "Deleted";
                grvHtmlFile.DataContext = null;
                grvHtmlFile.DataContext = listhtmlFile;
            }
            else
            {
                //System.Windows.MessageBox.Show("File not found");
                listhtmlFile[select].status = "File not found";
                grvHtmlFile.DataContext = null;
                grvHtmlFile.DataContext = listhtmlFile;
            }
        }

        private void btnRun_Click(object sender, RoutedEventArgs e)
        {

            int select = grvHtmlFile.SelectedIndex;
            string linkSelect = ((ItemHtmlSelect) grvHtmlFile.SelectedItem).link;
            if (File.Exists(linkSelect))
            {
                string Feedback = Run(linkSelect);
                listhtmlFile[select].status = Feedback;
                grvHtmlFile.DataContext = null;
                grvHtmlFile.DataContext = listhtmlFile;
            }
            else
            {
                //System.Windows.MessageBox.Show("File not found");
                listhtmlFile[select].status = "File not found";
                grvHtmlFile.DataContext = null;
                grvHtmlFile.DataContext = listhtmlFile;
            }
        }

        //private void UpdateProgress(int Process, int all)
        //{
        //    try
        //    {
        //        // Calculate the download progress in percentages
        //        float PercentProgress = (Process / all) * 100;
        //        // Make progress on the progress bar
        //        Running.Value = PercentProgress;
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show(ex.ToString());
        //    }
        //}
    }
}
