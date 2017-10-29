using NSoup;
using NSoup.Nodes;
using NSoup.Select;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace WebImageDownloader
{
    class eroticity: GenaralDownload
    {
        public eroticity(string path, string name, string url1)
        {
            savepath = path;
            savename = name;
            url = url1;
        }

        public override List<ItemDown> GetImagesLinkFromUrl()
        {
            List<ItemDown> listDown = new List<ItemDown>();

            //create a new folder
            string targetfolder = "";
            string directory;

            string foldername = CreateFolderName(this.url);

            targetfolder = savepath + "\\" + foldername; ;
            System.IO.Directory.CreateDirectory(targetfolder);

            //lay link galery tu link chung 
            List<string> listtemp = new List<string>();
            StreamReader inStream;
            WebRequest webRequest;
            WebResponse webresponse;
            webRequest = WebRequest.Create(url);
            webresponse = webRequest.GetResponse();
            inStream = new StreamReader(webresponse.GetResponseStream());
            String htmlstring = inStream.ReadToEnd();
            Document doc = NSoupClient.ParseBodyFragment(htmlstring);

            Elements Links = doc.Select("img");
            foreach (Element link in Links)
            {
                string imagelink = link.Parent.Attr("href");
                if (!(imagelink == "") && !(imagelink == "/"))
                {
                    listtemp.Add("https://fuskator.com" + imagelink);
                    //listtemp.Add(imagelink);
                }
            }
            foreach (string galerylink in listtemp)
            {

                //lay link anh tu cac link galery
                try
                {
                    WebRequest webRequest2 = WebRequest.Create(galerylink);
                    WebResponse webresponse2 = webRequest2.GetResponse();
                    StreamReader inStream2 = new StreamReader(webresponse.GetResponseStream());
                    String htmlstring2 = inStream2.ReadToEnd();
                    Document doc2 = NSoupClient.ParseBodyFragment(htmlstring2);

                    Elements Links2 = doc2.Select("img");

                    int i = 0;
                    //tach lay cai link image va down ve
                    foreach (Element link in Links2)
                    {
                        string imagelink = "https:" + link.Parent.Attr("src");
                        string savename = imagelink.Substring(imagelink.LastIndexOf("/") + 1);

                        directory = targetfolder + "\\" + savename;

                        //if (i < 10)
                        //directory = targetfolder + "\\" + savename + "00" + i + ".jpg";
                        //else if (i >= 10 && i < 100)
                        //directory = targetfolder + "\\" + savename + "0" + i + ".jpg";
                        //else directory = targetfolder + "\\" + savename + i + ".jpg";

                        ItemDown temp = new ItemDown(i, directory, imagelink, 0, "waiting");
                        listDown.Add(temp);
                        i++;
                    }

                }
                catch (Exception ex)
                {
                }
            }
            return listDown;
        }

        public void GetImagesLinkFromUrl2()
        {
            //List<ItemDown> listDown = new List<ItemDown>();
            StreamWriter sw = new StreamWriter("data.txt", false);

            //create a new folder
            string targetfolder = "";
            string directory;

            string foldername = url.Substring(url.LastIndexOf("/") + 1);

            targetfolder = savepath + "\\" + foldername; ;
            System.IO.Directory.CreateDirectory(targetfolder);

            //lay link galery tu link chung 
            List<string> listtemp = new List<string>();
            StreamReader inStream;
            WebRequest webRequest;
            WebResponse webresponse;
            webRequest = WebRequest.Create(url);
            webresponse = webRequest.GetResponse();
            inStream = new StreamReader(webresponse.GetResponseStream());
            String htmlstring = inStream.ReadToEnd();
            Document doc = NSoupClient.ParseBodyFragment(htmlstring);

            Elements Links = doc.Select("img");
            foreach (Element link in Links)
            {
                string imagelink = link.Parent.Attr("href");
                if (!(imagelink == "") && !(imagelink == "/") && imagelink.IndexOf("coreimg.net") >0)
                {
                    listtemp.Add(imagelink);
                    //listtemp.Add(imagelink);
                }
            }
            foreach (string galerylink in listtemp)
            {

                //lay link anh tu cac link galery
                try
                {
                    WebRequest webRequest2 = WebRequest.Create(galerylink);
                    WebResponse webresponse2 = webRequest2.GetResponse();
                    StreamReader inStream2 = new StreamReader(webresponse2.GetResponseStream());
                    String htmlstring2 = inStream2.ReadToEnd();
                    Document doc2 = NSoupClient.ParseBodyFragment(htmlstring2);

                    Elements Links2 = doc2.Select("img");

                    int i = 0;
                    //tach lay cai link image va down ve
                    foreach (Element link in Links2)
                    {
                        string imagelink = link.Attr("src");
                        //string savename = imagelink.Substring(imagelink.LastIndexOf("/") + 1);

                        //directory = targetfolder + "\\" + savename;

                        string savename = "";
                        if (i < 10)
                        directory = targetfolder + "\\" + savename + "00" + i + ".jpg";
                        else if (i >= 10 && i < 100)
                        directory = targetfolder + "\\" + savename + "0" + i + ".jpg";
                        else directory = targetfolder + "\\" + savename + i + ".jpg";

                        //ItemDown temp = new ItemDown(i, directory, imagelink, 0, "waiting");
                        //listDown.Add(temp);

                        sw.WriteLine(i + "#" + directory + "#" + imagelink + "#" + 0 + "#waiting");
                        i++;
                    }

                }
                catch (Exception ex)
                {
                }
            }
            sw.Close();
        }

        

    }
}
