using NSoup;
using NSoup.Nodes;
using NSoup.Select;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace WebImageDownloader
{
    class urlgalleriesDownload:GenaralDownload
    { 
        public urlgalleriesDownload(string path, string name, string url1)
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

            //tạo vị trí lưu
            string foldername = CreateFolderName(url);
            foldername = foldername.Replace("\\", "").Replace("/", "").Replace(":", "").Replace("*", "").Replace("?", "").
                Replace("\"", "").Replace("~", "").Replace("<", "").Replace(">", "").Replace("|", "").Replace(".", "").Replace("+", "");
            targetfolder = savepath + "\\" + foldername;
            System.IO.Directory.CreateDirectory(targetfolder);

            StreamReader inStream;
            WebRequest webRequest;
            WebResponse webresponse;
            webRequest = WebRequest.Create(url);
            webresponse = webRequest.GetResponse();
            inStream = new StreamReader(webresponse.GetResponseStream());
            String htmlstring = inStream.ReadToEnd();
            Document doc = NSoupClient.ParseBodyFragment(htmlstring);

            List<string> listTemp = new List<string>();

            Elements Links = doc.Select("a");

            //string lastListLink = "";
            foreach (Element link in Links)
            {
                if ("gallerybody".Equals(link.Parent.Parent.Parent.Parent.Parent.Parent.ClassName()))
                {
                    string imageLink = "http://litugirls.urlgalleries.net" + link.Attr("href");
                    listTemp.Add(imageLink);
                }
            }


            foreach (string item in listTemp)
            {
                //down
                WebRequest webRequest2 = WebRequest.Create(item);
                WebResponse webresponse2 = webRequest2.GetResponse();
                StreamReader inStream2 = new StreamReader(webresponse2.GetResponseStream());
                String htmlstring2 = inStream2.ReadToEnd();
                Document doc2 = NSoupClient.ParseBodyFragment(htmlstring2);

                Elements linkdown = doc2.Select("iframe");
               

                int i = 0;
                //lay ra link trang chua anh, tu link nay moilay ra duoc anh, rat vai
                foreach (Element link in linkdown)
                {
                    if (link.Id.Equals("thepic"))                       
                    {
                        i++;
                        string imagelink = "http://img227.imagevenue.com/" + link.Attr("src");
                        //imagelink = imagelink.Replace("/thumbs/thumbs_", "/");
                       
                        int int1 = imagelink.LastIndexOf("/") + 1;
                        int int2 = imagelink.LastIndexOf(".");
                        directory = targetfolder + @"\" + imagelink.Substring(int1, int2-int1) + ".jpg";

                        ItemDown temp = new ItemDown(i,directory, imagelink,0, "waiting");
                        listDown.Add(temp);
                        
                    }
                }
            }

            return listDown;
        }


        public override void GetImagesLinkFromUrl2()
        {
            //List<ItemDown> listDown = new List<ItemDown>();
            StreamWriter sw = new StreamWriter("data.txt", false);

            //create a new folder
            string targetfolder = "";
            string directory;

            //tạo vị trí lưu
            string foldername = CreateFolderName(url);
            foldername = foldername.Replace("\\", "").Replace("/", "").Replace(":", "").Replace("*", "").Replace("?", "").
                Replace("\"", "").Replace("~", "").Replace("<", "").Replace(">", "").Replace("|", "").Replace(".", "").Replace("+", "");
            targetfolder = savepath + "\\" + foldername;
            System.IO.Directory.CreateDirectory(targetfolder);

            StreamReader inStream;
            WebRequest webRequest;
            WebResponse webresponse;
            webRequest = WebRequest.Create(url);
            webresponse = webRequest.GetResponse();
            inStream = new StreamReader(webresponse.GetResponseStream());
            String htmlstring = inStream.ReadToEnd();
            Document doc = NSoupClient.ParseBodyFragment(htmlstring);

            List<string> listTemp = new List<string>();

            Elements Links = doc.Select("a");

            //string lastListLink = "";
            foreach (Element link in Links)
            {
                if ("gallerybody".Equals(link.Parent.Parent.Parent.Parent.Parent.Parent.ClassName()))
                {
                    string imageLink = "http://litugirls.urlgalleries.net" + link.Attr("href");
                    listTemp.Add(imageLink);
                }
            }


            foreach (string item in listTemp)
            {
                //down
                WebRequest webRequest2 = WebRequest.Create(item);
                WebResponse webresponse2 = webRequest2.GetResponse();
                StreamReader inStream2 = new StreamReader(webresponse2.GetResponseStream());
                String htmlstring2 = inStream2.ReadToEnd();
                Document doc2 = NSoupClient.ParseBodyFragment(htmlstring2);

                Elements linkdown = doc2.Select("iframe");


                int i = 0;
                //lay ra link trang chua anh, tu link nay moilay ra duoc anh, rat vai
                foreach (Element link in linkdown)
                {
                    if (link.Id.Equals("thepic"))
                    {
                        i++;
                        string imagelink = "http://img227.imagevenue.com/" + link.Attr("src");
                        //imagelink = imagelink.Replace("/thumbs/thumbs_", "/");

                        int int1 = imagelink.LastIndexOf("/") + 1;
                        int int2 = imagelink.LastIndexOf(".");
                        directory = targetfolder + @"\" + imagelink.Substring(int1, int2 - int1) + ".jpg";

                        //ItemDown temp = new ItemDown(i, directory, imagelink, 0, "waiting");
                        //listDown.Add(temp);
                        sw.WriteLine(i + "#" + directory + "#" + imagelink + "#" + 0 + "#waiting");

                    }
                }
            }
            sw.Close();
            //return listDown;
        }
       
    }
}

   