using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
//using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Threading;

namespace WebImageDownloader
{
    /// <summary>
    /// Interaction logic for WID.xaml
    /// </summary>
    public partial class WID : Window
    {
        #region Variable
        private Task taskMainDown;
        // create the cancellation token source
        private static CancellationTokenSource tokenSource = new CancellationTokenSource();
        // create the cancellation token
        private CancellationToken token = tokenSource.Token;

        private bool isRunning = false;

        //private List<ItemDown> MainListItemsDownload;
        private List<ItemDown> MainQueueItemDownload;
        //private Queue<ItemDown> MainQueueDownload;

        public string SaveName { set; get; }
        public string SaveLink { set; get; }

        #endregion
        public WID()
        {
            InitializeComponent();
            MainQueueItemDownload = new List<ItemDown>();
            SaveName = "Anh";
            SaveLink = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        #region Function

        private void Download(ItemDown _ItemDown)
        {
            //ItemDown _ItemDown = (ItemDown)ItemDown;
            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)HttpWebRequest.Create(_ItemDown.linkdown);
                HttpWebResponse httpWebReponse = (HttpWebResponse)httpWebRequest.GetResponse();
                Stream stream = httpWebReponse.GetResponseStream();
                Image image = Image.FromStream(stream);
                if (image != null)
                {
                    //lets save image to disk                      
                    image.Save(_ItemDown.savepath);
                    _ItemDown.status = "Completed";
                    _ItemDown.percentage = 100;
                    DataGridListDownload.Dispatcher.Invoke(DispatcherPriority.Background, new Action(delegate
                        {
                            DataGridListDownload.DataContext = null;
                            DataGridListDownload.DataContext = MainQueueItemDownload;
                        }));
                }
            }

            catch (Exception _ex)
            {
                _ItemDown.status = "Error: " + _ex.ToString();
            }
        }

        //can xem lai, trong truong hop clink ko lien nhau thi sao
        private void Delete()
        {
            if (!isRunning)
            {
                int StartSelect = DataGridListDownload.SelectedIndex;
                int RangeSelect = DataGridListDownload.SelectedItems.Count;
                MainQueueItemDownload.RemoveRange(StartSelect - RangeSelect + 1, RangeSelect);
                DataGridListDownload.DataContext = null;
                DataGridListDownload.DataContext = MainQueueItemDownload;
            }
            else System.Windows.MessageBox.Show("Can't delete when download");
        }

        private void Clear()
        {
            if (!isRunning)
            {
                DataGridListDownload.DataContext = null;
                //MainListItemsDownload.Clear();
                MainQueueItemDownload.Clear();
            }
            else
            {
                MessageBox.Show("Can't clear list when downloading");
            }
        }

        private void StartDownload()
        {
            ButtonAdd.IsEnabled = false;
           
            tokenSource.Cancel();
            isRunning = true;

            foreach (ItemDown item in MainQueueItemDownload) //MainListItemsDownload
            {
                if (!item.status.Equals("Completed"))
                    item.status = "Downloading...";
            }
            DataGridListDownload.DataContext = null;
            DataGridListDownload.DataContext = MainQueueItemDownload;// MainListItemsDownload;
            //TaskScheduler uiScheduler = TaskScheduler.FromCurrentSynchronizationContext();

            taskMainDown = Task.Factory.StartNew(() =>
            {
                int i = 0;

                while (true)
                {
                    if (i + 10 < MainQueueItemDownload.Count)
                    {
                        Parallel.For(i, i + 10, (k) =>
                        {
                            if (!MainQueueItemDownload[k].status.Equals("Completed"))
                                Download(MainQueueItemDownload[k]);
                            //token.ThrowIfCancellationRequested();
                            DataGridListDownload.Dispatcher.Invoke(DispatcherPriority.Background, new Action(delegate
                                {
                                    DataGridListDownload.ScrollIntoView(DataGridListDownload.Items[k]);
                                }));
                        });

                        i = i + 10;
                    }
                    else
                    {
                        Parallel.For(i, MainQueueItemDownload.Count, (k) =>
                        {
                            if (!MainQueueItemDownload[k].status.Equals("Completed"))
                                Download(MainQueueItemDownload[k]);
                            //token.ThrowIfCancellationRequested();
                        });

                        break;
                    }
                }
            }).ContinueWith(ant =>
            {
                ButtonAdd.IsEnabled = true;
                isRunning = false;
                MessageBox.Show("Done", "Download is completed!"); 
            }, TaskScheduler.FromCurrentSynchronizationContext());
        
        }

        private void Pause()
        {
            tokenSource.Cancel();
            isRunning = false;
        }

        public void AddReturn()
        {
            ButtonAdd.IsEnabled = false;

            //Task taskCreateList =
            //Task.Factory.StartNew(() =>
            //{
            DataGridListDownload.DataContext = null;
                StreamReader sr = new StreamReader("data.txt");
                        string input = null;
                        while ((input = sr.ReadLine()) != null)
                        {
                            string[] itemSplit = input.Split('#');
                            ItemDown item = new ItemDown(int.Parse(itemSplit[0]), itemSplit[1], itemSplit[2], int.Parse(itemSplit[3]), itemSplit[4]);
                            if (!"".Equals(item.linkdown))

                                MainQueueItemDownload.Add(item);
                                //MainQueueDownload.Enqueue(item);
                        }
                        sr.Close();
                   // }).ContinueWith(ant =>
                    //{
                        DataGridListDownload.DataContext = MainQueueItemDownload;// MainListItemsDownload;
                        ButtonAdd.IsEnabled = true;
                    //}, TaskScheduler.FromCurrentSynchronizationContext());
        }

        #endregion

        #region Event From
        

        private void ButtonAdd_Click(object sender, RoutedEventArgs e)
        {
            AddItem addForm = new AddItem(this);
            addForm.ShowDialog();
        }

        private void ComboBox_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if(comboBox_SelectDeleteMethod.SelectedIndex==0)
            {
                image_Delete_Button.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "/Images/Button-Delete.ico"));
                textbox_Delete_Button.Text = "Delete";
            }

            else
            {
                image_Delete_Button.Source = new BitmapImage(new Uri(Directory.GetCurrentDirectory() + "/Images/Button-DeleteAll2.ico"));
                textbox_Delete_Button.Text = "Delete All";
            }
        }

        private void ButtonDown_Click(object sender, RoutedEventArgs e)
        {
            StartDownload();
        }

        private void ButtonDelete_Click(object sender, RoutedEventArgs e)
        {
            if(comboBox_SelectDeleteMethod.SelectedIndex == 0)
            {
                Delete();
            }

            else
            {
                Clear();
            }
        }

        private void ButtonOptions_Click(object sender, RoutedEventArgs e)
        {
            Options optionsForm = new Options(this);
            optionsForm.ShowDialog();
        }

        private void ButtonHtml_Click(object sender, RoutedEventArgs e)
        {
            SaveImageFromHtml saveimageForm = new SaveImageFromHtml();
            saveimageForm.ShowDialog();
        }

        private void ButtonPDF_Click(object sender, RoutedEventArgs e)
        {
            ChapToVol chaptovol = new ChapToVol();
            chaptovol.ShowDialog();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Delete();
        }

        private void ButtonPause_Click(object sender, RoutedEventArgs e)
        {
            Pause();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            AddItem addForm = new AddItem(this);
            addForm.ShowDialog();
        }

        private void MenuItem_Click_2(object sender, RoutedEventArgs e)
        {
            StartDownload();
        }

        private void MenuItem_Click_3(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void MenuItem_Click_4(object sender, RoutedEventArgs e)
        {
            Delete();
        }

        private void MenuItem_Click_5(object sender, RoutedEventArgs e)
        {
            Clear();
        }

        private void MenuItem_Click_6(object sender, RoutedEventArgs e)
        {
            Options optionsForm = new Options(this);
            optionsForm.ShowDialog();
        }

        private void myGif_MediaEnded(object sender, RoutedEventArgs e)
        {
            myGif.Position = new TimeSpan(0, 0, 1);
            myGif.Play();
        }

        private void Run_Gif(object sender, RoutedEventArgs e)
        {
            myGif.Position = new TimeSpan(0, 0, 1);
            myGif.Play();
        }

        #endregion

       
    }
}
