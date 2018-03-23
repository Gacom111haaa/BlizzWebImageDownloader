using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;
using System.Xml.Linq;

namespace WebImageDownloader
{
    /// <summary>
    /// Interaction logic for Options.xaml
    /// </summary>
    public partial class Options : Window
    {
        private List<filterOb> listFilters = new List<filterOb>();
        private WID main;
        public Options()
        {
            LoadFilter();
            InitializeComponent();
        }

        public Options(WID _main)
        {
            main = _main;
            InitializeComponent();
            textBox_savePath.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            textBox_saveName.Text = "Anh";
            LoadFilter();
        }

        private void button_OKOptions_Click(object sender, RoutedEventArgs e)
        {
            main.SaveName = textBox_saveName.Text;
            main.SaveLink = textBox_savePath.Text;
            this.Close();
        }

        private void button_CancelOptions_Click(object sender, RoutedEventArgs e)
        {
            this.Close();            
        }

        private void button_OptionsBrower_Click(object sender, RoutedEventArgs e)
        {
            FolderBrowserDialog fl = new FolderBrowserDialog();
            fl.SelectedPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            fl.ShowNewFolderButton = true;
            if (fl.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                textBox_savePath.Text = fl.SelectedPath;
            }    
        }

        private void LoadFilter()
        {
            XElement xDoc = XElement.Load("Filter.xml");
            listFilters = (from q in xDoc.Elements("Filter")
                           select new filterOb(q.Element("Link").Value, q.Element("Source").Value, q.Element("Target").Value)
                            ).ToList();
            listboxFilter.DataContext = listFilters;
        }

        private void btnAddFilter_Click(object sender, RoutedEventArgs e)
        {
            if (tbxLink.Text == "")
            {
                System.Windows.MessageBox.Show("Link is not defined");
            }
            else if (tbxSource.Text == "")
            {
                System.Windows.MessageBox.Show("Source is not defined");
            }
            else if(tbxTarget.Text == "")
            {
                System.Windows.MessageBox.Show("Target is not defined");
            }
            else
            {
                string link = tbxLink.Text.Trim();
                string source = tbxSource.Text.Trim();
                string target = tbxTarget.Text.Trim();

                XDocument xmlDoc = XDocument.Load("Filter.xml");

                xmlDoc.Element("Filters").Add(
                    new XElement("Filter",
                        new XElement("Link", link),
                        new XElement("Source", source),
                        new XElement("Target", target)
                        )
                    );
                xmlDoc.Save("Filter.xml");

                tbxLink.Text = "";
                tbxSource.Text = "";
                tbxTarget.Text = "";
                listboxFilter.DataContext = null;
                LoadFilter();
            }
        }
    }

    public class filterOb
    {
        public string link { set; get; }
        public string source { set; get; }
        public string target { set; get; }

        public filterOb(string _link,string _source, string _target)
        {
            link = _link;
            source = _source;
            target = _target;
        }
    }

}
