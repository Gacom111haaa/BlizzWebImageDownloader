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

        public GenaralDownload(){}

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

                    ItemDown temp = new ItemDown(i,directory, imagelink, 0,"waiting");
                    listImage.Add(temp);
                    i++;  
                }
                
                return listImage;
            }         
            catch (Exception _ex)
            {
                StreamWriter sw = new StreamWriter(Directory.GetCurrentDirectory() + "log.txt",true);
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

        
       
    }
}
