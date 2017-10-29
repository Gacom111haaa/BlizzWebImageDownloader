using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using NSoup;
using NSoup.Nodes;
using NSoup.Select;

namespace WebImageDownloader
{
    class GenaralDownload
    {
        public string savepath { set; get; }
        public string savename { set; get; }
        public string url { set; get; }

        public GenaralDownload() { }

        public GenaralDownload(string path, string name, string url1)
        {
            savepath = path;
            savename = name;
            url = url1;
        }

        public virtual string CreateFolderName(string _url)
        {
            if (!"".Equals(_url))
            {
                //tao folder name
                string foldername = "";
                if (_url.LastIndexOf("/") == _url.Length - 1)
                {
                    foldername = _url.Substring(0, _url.Length - 1);
                    foldername = foldername.Substring(foldername.LastIndexOf("/") + 1);
                }
                else if (_url.LastIndexOf(".html") > 0)
                {
                    int temp1 = _url.LastIndexOf("/") + 1;
                    int temp2 = _url.LastIndexOf(".");
                    foldername = _url.Substring(temp1, temp2 - temp1);
                }
                else foldername = _url.Substring(_url.LastIndexOf("/") + 1);

                return foldername;
            }
            else return null;
        }

        public virtual List<ItemDown> GetImagesLinkFromUrl()
        {
            List<ItemDown> listImage = new List<ItemDown>();

            //create a new folder
            string targetfolder = "";
            string directory;

            string foldername = CreateFolderName(this.url);


            try
            {
                targetfolder = savepath + "\\" + foldername;
                System.IO.Directory.CreateDirectory(targetfolder);
            }
            catch (Exception ex)
            {
                targetfolder = savepath + "\\Test";
                System.IO.Directory.CreateDirectory(targetfolder);
            }

            try
            {
                HttpWebRequest myWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                myWebRequest.Method = "GET";
                myWebRequest.UserAgent = "Foo";
                myWebRequest.Accept = "text/html";
                HttpWebResponse myWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
                StreamReader myWebSource = new StreamReader(myWebResponse.GetResponseStream());
                string htmlstring = string.Empty;
                htmlstring = myWebSource.ReadToEnd();
                myWebResponse.Close();
                Document doc = NSoupClient.ParseBodyFragment(htmlstring);

                Elements Links = doc.Select("img");

                int i = 0;
                //tach lay cai link image va down ve
                foreach (Element link in Links)
                {

                    string imagelink = link.Attr("abs:src");
                    if (i < 10)
                        directory = targetfolder + "\\" + savename + "00" + i + ".jpg";
                    else if (i >= 10 && i < 100)
                        directory = targetfolder + "\\" + savename + "0" + i + ".jpg";
                    else directory = targetfolder + "\\" + savename + i + ".jpg";

                    ItemDown temp = new ItemDown(i, directory, imagelink, 0, "waiting");
                    listImage.Add(temp);
                    i++;
                }

                return listImage;
            }
            catch (Exception _ex)
            {
                StreamWriter sw = new StreamWriter(Directory.GetCurrentDirectory() + "log.txt", true);
                sw.WriteLine("Lỗi khi lấy link ảnh:");
                sw.Write(_ex.ToString());
                sw.Close();
                return null;
            }

        }

        public virtual void GetImagesLinkFromUrl2()
        {
            //List<ItemDown> listImage = new List<ItemDown>();
            StreamWriter sw = new StreamWriter("data.txt", false);
            //create a new folder
            string targetfolder = "";
            string directory;

            string foldername = CreateFolderName(this.url);


            try
            {
                targetfolder = savepath + "\\" + foldername;
                System.IO.Directory.CreateDirectory(targetfolder);
            }
            catch (Exception ex)
            {
                targetfolder = savepath + "\\Test";
                System.IO.Directory.CreateDirectory(targetfolder);
            }

            try
            {
                HttpWebRequest myWebRequest = (HttpWebRequest)HttpWebRequest.Create(url);
                myWebRequest.Method = "GET";
                myWebRequest.UserAgent = "Foo";
                myWebRequest.Accept = "text/html";
                HttpWebResponse myWebResponse = (HttpWebResponse)myWebRequest.GetResponse();
                StreamReader myWebSource = new StreamReader(myWebResponse.GetResponseStream());
                string htmlstring = string.Empty;
                htmlstring = myWebSource.ReadToEnd();
                myWebResponse.Close();
                Document doc = NSoupClient.ParseBodyFragment(htmlstring);

                Elements Links = doc.Select("img");

                int i = 0;
                //tach lay cai link image va down ve
                foreach (Element link in Links)
                {

                    string imagelink = link.Attr("abs:src");
                    if (i < 10)
                        directory = targetfolder + "\\" + savename + "00" + i + ".jpg";
                    else if (i >= 10 && i < 100)
                        directory = targetfolder + "\\" + savename + "0" + i + ".jpg";
                    else directory = targetfolder + "\\" + savename + i + ".jpg";

                    //ItemDown temp = new ItemDown(i, directory, imagelink, 0, "waiting");
                    //listImage.Add(temp);
                    sw.WriteLine(i + "#" + directory + "#" + imagelink + "#" + 0 + "#waiting");
                    i++;
                }

                //return listImage;
                sw.Close();
            }
            catch (Exception _ex)
            {
                StreamWriter sw2 = new StreamWriter(Directory.GetCurrentDirectory() + "log.txt", true);
                sw2.WriteLine("Lỗi khi lấy link ảnh:");
                sw2.Write(_ex.ToString());
                sw2.Close();
                sw.Close();
            }

        }

        public virtual void GetImagesLinkFromUrl3()
        {
            //List<ItemDown> listImage = new List<ItemDown>();
            StreamWriter sw = new StreamWriter("data.txt", false);
            //create a new folder
            string targetfolder = "";
            string directory;

            string foldername = CreateFolderName(this.url);


            try
            {
                targetfolder = savepath + "\\" + foldername;
                System.IO.Directory.CreateDirectory(targetfolder);
            }
            catch (Exception ex)
            {
                targetfolder = savepath + "\\Test";
                System.IO.Directory.CreateDirectory(targetfolder);
            }

            try
            {
                string htmlstring = string.Empty;
                htmlstring = File.ReadAllText(url);
                Document doc = NSoupClient.ParseBodyFragment(htmlstring);

                Elements Links = doc.Select("img");

                int i = 0;
                //tach lay cai link image va down ve
                foreach (Element link in Links)
                {

                    string imagelink = link.Attr("abs:src");
                    if (i < 10)
                        directory = targetfolder + "\\" + savename + "00" + i + ".jpg";
                    else if (i >= 10 && i < 100)
                        directory = targetfolder + "\\" + savename + "0" + i + ".jpg";
                    else directory = targetfolder + "\\" + savename + i + ".jpg";

                    //ItemDown temp = new ItemDown(i, directory, imagelink, 0, "waiting");
                    //listImage.Add(temp);
                    sw.WriteLine(i + "#" + directory + "#" + imagelink + "#" + 0 + "#waiting");
                    i++;
                }

                //return listImage;
                sw.Close();
            }
            catch (Exception _ex)
            {
                StreamWriter sw2 = new StreamWriter(Directory.GetCurrentDirectory() + "log.txt", true);
                sw2.WriteLine("Lỗi khi lấy link ảnh:");
                sw2.Write(_ex.ToString());
                sw2.Close();
                sw.Close();
            }
        }

        public void GetImagesLinkFromUrl4()
        {
            //List<ItemDown> listDown = new List<ItemDown>();
            StreamWriter sw = new StreamWriter("data.txt", false);

            //create a new folder
            string targetfolder = "";
            string directory;

            string foldername = url.Substring(url.LastIndexOf("\\") + 1);

            targetfolder = savepath + "\\" + foldername; ;
            System.IO.Directory.CreateDirectory(targetfolder);

            //lay link galery tu link chung 
            List<string> listtemp = new List<string>();

            String htmlstring = File.ReadAllText(url);
            Document doc = NSoupClient.ParseBodyFragment(htmlstring);

            Elements Links = doc.Select("a");
            foreach (Element link in Links)
            {
                string imagelink = link.Attr("href");
                if (!(imagelink == "") && !(imagelink == "/") && imagelink.IndexOf("https://www.pixhost.org") >= 0)
                {
                    listtemp.Add(imagelink.Replace("https://www.pixhost.org/show/", "https://img1.pixhost.org/images/"));
                }
            }
            int i = 0;
            foreach (string imagelink in listtemp)
            {
                try
                {

                    string savename = "";
                    if (i < 10)
                        directory = targetfolder + "\\" + savename + "00" + i + ".jpg";
                    else if (i >= 10 && i < 100)
                        directory = targetfolder + "\\" + savename + "0" + i + ".jpg";
                    else directory = targetfolder + "\\" + savename + i + ".jpg";
                    sw.WriteLine(i + "#" + directory + "#" + imagelink + "#" + 0 + "#waiting");
                    i++;
                }
                catch (Exception ex)
                {
                }
            }
            sw.Close();
        }

        public void GetImagesLinkFromUrl5()
        {
            //List<ItemDown> listDown = new List<ItemDown>();
            StreamWriter sw = new StreamWriter("data.txt", false);

            //create a new folder
            string targetfolder = "";
            string directory;

            string foldername = url.Substring(url.LastIndexOf("\\") + 1);

            targetfolder = savepath + "\\" + foldername; ;
            System.IO.Directory.CreateDirectory(targetfolder);

            //lay link galery tu link chung 
            List<string> listtemp = new List<string>();

            String htmlstring = File.ReadAllText(url);
            Document doc = NSoupClient.ParseBodyFragment(htmlstring);

            Elements Links = doc.Select("img");
            foreach (Element link in Links)
            {
                string imagelink = link.Attr("src");
                if (!(imagelink == "") && !(imagelink == "/") && imagelink.IndexOf("http://imgmaid.net/t/") >= 0)
                {
                    listtemp.Add(imagelink.Replace("http://imgmaid.net/t/", "http://imgmaid.net/i/"));
                }
                else if (!(imagelink == "") && !(imagelink == "/") && imagelink.IndexOf("http://t2.imgchili.com") >= 0)
                {
                    listtemp.Add(imagelink.Replace("http://t2.imgchili.com", "http://i2.imgchili.net/")); 
                }
                else if (!(imagelink == "") && !(imagelink == "/") && imagelink.IndexOf("http://fireimg.cc/upload/small") >= 0)
                {
                    listtemp.Add(imagelink.Replace("http://fireimg.cc/upload/small", "http://i.fireimg.cc/big"));
                }
                else if (!(imagelink == "") && !(imagelink == "/") && imagelink.IndexOf("http://t10.imgchili.net/") >= 0)
                {
                    listtemp.Add(imagelink.Replace("http://t10.imgchili.net/", "http://i10.imgchili.net/"));
                }
                else if (!(imagelink == "") && !(imagelink == "/") && imagelink.IndexOf("http://coreimg.net/t") >= 0)
                {
                    listtemp.Add(imagelink.Replace("http://coreimg.net/t", "http://coreimg.net/i"));
                }
                else if (!(imagelink == "") && !(imagelink == "/") && imagelink.IndexOf("https://t1.pixhost.org/thumbs/") >= 0)
                {
                    listtemp.Add(imagelink.Replace("https://t1.pixhost.org/thumbs/", "https://img1.pixhost.org/images/"));
                }
                else if (!(imagelink == "") && !(imagelink == "/") && imagelink.IndexOf("http://fapat.me/upload/small/") >= 0)
                {
                    listtemp.Add(imagelink.Replace("http://fapat.me/upload/small/", "http://fapat.me/upload/big/"));
                }
                else if (!(imagelink == "") && !(imagelink == "/") && imagelink.IndexOf("http://imgcherry.org/upload/small/") >= 0)
                {
                    listtemp.Add(imagelink.Replace("http://imgcherry.org/upload/small/", "http://imgcherry.org/upload/big/"));
                }
                else if (!(imagelink == "") && !(imagelink == "/") && imagelink.IndexOf("http://imgsen.se/upload/small/") >= 0)
                {
                    listtemp.Add(imagelink.Replace("/small/", "/big/"));
                }
            }
            int i = 0;
            foreach (string imagelink in listtemp)
            {
                try
                {

                    string savename = "";
                    if (i < 10)
                        directory = targetfolder + "\\" + savename + "00" + i + ".jpg";
                    else if (i >= 10 && i < 100)
                        directory = targetfolder + "\\" + savename + "0" + i + ".jpg";
                    else directory = targetfolder + "\\" + savename + i + ".jpg";
                    sw.WriteLine(i + "#" + directory + "#" + imagelink + "#" + 0 + "#waiting");
                    i++;
                }
                catch (Exception ex)
                {
                }
            }
            sw.Close();
        }

    }
}
