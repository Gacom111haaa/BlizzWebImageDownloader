using System;
using System.Collections.Generic;
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

namespace WebImageDownloader
{
    /// <summary>
    /// Interaction logic for Options.xaml
    /// </summary>
    public partial class Options : Window
    {
        private WID main;
        public Options()
        {
            InitializeComponent();
        }

        public Options(WID _main)
        {
            main = _main;
            InitializeComponent();
            textBox_savePath.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            textBox_saveName.Text = "Anh";
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
    }
}
