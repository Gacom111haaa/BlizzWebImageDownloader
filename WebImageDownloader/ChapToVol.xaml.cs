using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using iTextSharp.text;
using iTextSharp.text.pdf;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Threading;

namespace WebImageDownloader
{
    /// <summary>
    /// Interaction logic for ChapToVol.xaml
    /// </summary>
    public partial class ChapToVol : Window
    {
        public ChapToVol()
        {
            InitializeComponent();
            textBoxOutput.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            string ThumucVao = textBoxInput.Text;
            int NumterChapPerVol = 1;// Int32.Parse(textBoxNumberChap.Text);
            int ChapStart = 1;// Int32.Parse(textBoxChapStart.Text);
            string VitriFileOut = textBoxOutput.Text;
            string tenTruyen = textBoxName.Text;

            string[] alldirec = Directory.GetDirectories(ThumucVao);
            List<string> data = new List<string>();

            //Thread Runthread = new Thread()

            progressBar1.Minimum = 0;
            progressBar1.Maximum = alldirec.Length;
            progressBar1.Value = 0;


            for (int i = (ChapStart - 1); i < alldirec.Length; i++)
            {
                if ((i + 1) % NumterChapPerVol == 0)
                {
                    data.Add(alldirec[i]);

                    Document doc = new Document();
                    //PdfWriter.GetInstance(doc, new FileStream(VitriFileOut + @"\" + tenTruyen + @"_vol_" + ((i + 1) / NumterChapPerVol) + ".pdf", FileMode.Create));
                    PdfWriter.GetInstance(doc, new FileStream(VitriFileOut + @"\" + alldirec[i].Substring(alldirec[i].LastIndexOf(@"\")) + ".pdf", FileMode.Create));
                    doc.Open();

                    foreach (string dataline in data)
                    {
                        string[] allfiles = Directory.GetFiles(dataline);
                        foreach (string file in allfiles)
                        {
                            try
                            {
                                iTextSharp.text.Image myImage = iTextSharp.text.Image.GetInstance(file);

                                if (myImage.Width < myImage.Height && myImage.Width > 100)
                                {

                                    doc.SetPageSize(iTextSharp.text.PageSize.A4);
                                    doc.NewPage();
                                }
                                else if (myImage.Width > 100)
                                {
                                    //phai set page roi moi dc tao new page
                                    doc.SetPageSize(iTextSharp.text.PageSize.A3.Rotate());
                                    doc.NewPage();
                                }

                                else
                                { //do nothing}
                                }

                                myImage.ScaleToFit(doc.PageSize.Width - 40f, doc.PageSize.Height - 40f);
                                doc.Add(myImage);
                            }
                            catch (Exception ex)
                            {
                                System.Windows.MessageBox.Show(ex.ToString());
                            }
                        }
                    }

                    doc.Close();
                    data.Clear();

                    Thread.Sleep(100);
                }
                else
                {
                    data.Add(alldirec[i]);
                }
                progressBar1.Dispatcher.Invoke(DispatcherPriority.Background, new Action(delegate
                {
                    progressBar1.Value++;
                }));

            }
        }

        private void button_Input_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fl = new FolderBrowserDialog();
            fl.SelectedPath = System.IO.Directory.GetCurrentDirectory();
            fl.ShowNewFolderButton = true;
            if (fl.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBoxInput.Text = fl.SelectedPath;
            }
        }

        private void button_Output_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fl = new FolderBrowserDialog();
            fl.SelectedPath = System.IO.Directory.GetCurrentDirectory();
            fl.ShowNewFolderButton = true;
            if (fl.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBoxOutput.Text = fl.SelectedPath;
            }
        }
    }

}