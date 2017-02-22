using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using NSoup;
using NSoup.Nodes;
using NSoup.Select;

namespace WebImageDownloader
{
    class BCFDownload : GenaralDownload
    {
        public BCFDownload(string path, string name, string url1)
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
            targetfolder = savepath + "\\" + foldername; ;
            System.IO.Directory.CreateDirectory(targetfolder);

            //lay cac duong link gallery
            WebRequest webRequest = WebRequest.Create(url);
            WebResponse webresponse = webRequest.GetResponse();
            StreamReader inStream = new StreamReader(webresponse.GetResponseStream());
            String htmlstring = inStream.ReadToEnd();
            Document doc = NSoupClient.ParseBodyFragment(htmlstring);

            List<string> listTemp = new List<string>();
            Elements Links = doc.Select("a");

            //loc ra duong link cuoi cung
            string lastListLink = "";
            foreach (Element link in Links)
            {
                if ("page-numbers".Equals(link.ClassName()))
                {
                    lastListLink = "http://www.bcfakes.com" + link.Attr("href");
                    //listTemp.Add(lastListLink);
                }
            }

            string temptest = "";
            if (!lastListLink.Equals(""))
            {
                temptest = lastListLink.Substring(lastListLink.IndexOf("?nggpage=") + 9);

                int lastListIndex = Int32.Parse(temptest);
                for (int i = 1; i <= lastListIndex; i++)
                {
                    string temp = lastListLink.Substring(0, lastListLink.IndexOf("?nggpage=") + 9) + i;
                    listTemp.Add(temp);
                }
            }
            else listTemp.Add(url);

            int count = 0;

            foreach (string item in listTemp)
            {
                //down
                WebRequest webRequest2 = WebRequest.Create(item);
                WebResponse webresponse2 = webRequest2.GetResponse();
                StreamReader inStream2 = new StreamReader(webresponse2.GetResponseStream());
                String htmlstring2 = inStream2.ReadToEnd();
                Document doc2 = NSoupClient.ParseBodyFragment(htmlstring2);

                Elements linkdown = doc2.Select("img");
               
                //lay ra link trang chua anh, tu link nay moilay ra duoc anh, rat vai
                foreach (Element link in linkdown)
                {
                    if ((link.Parent.Parent.Parent.Parent.ClassName().Equals("ngg-galleryoverview")
                        && link.Parent.Parent.Parent.Parent.Id.Equals("ngg-gallery-43-267"))
                        || link.Parent.Parent.ClassName().Equals("ngg-gallery-thumbnail"))
                    {
                        //string imagelink = link.Attr("src");
                        //imagelink = imagelink.Replace("/thumbs/thumbs_", "/");
                        string imagelink = link.Parent.Attr("href");
                        imagelink = "http://www.bcfakes.com" + imagelink;

                        WebRequest webRequest3 = WebRequest.Create(imagelink);
                        WebResponse webresponse3 = webRequest3.GetResponse();
                        StreamReader inStream3 = new StreamReader(webresponse3.GetResponseStream());
                        String htmlstring3 = inStream3.ReadToEnd();
                        Document doc3 = NSoupClient.ParseBodyFragment(htmlstring3);

                        Elements linkdownFinals = doc3.Select("a");

                        int countItem = 0;
                        foreach (Element linkdownFinal in linkdownFinals)
                        {
                            if (linkdownFinal.Parent.ClassName().Equals("pic"))
                            {
                                countItem ++;
                                string imagedownloadlink = linkdownFinal.Attr("href");
                                int int1 = imagedownloadlink.LastIndexOf("/") + 1;
                                int int2 = imagedownloadlink.LastIndexOf(".");
                                directory = targetfolder + @"\" + imagedownloadlink.Substring(int1, int2 - int1) + ".jpg";

                                ItemDown temp = new ItemDown(countItem, directory, imagedownloadlink, 0,"waiting");
                                listDown.Add(temp);
                                count++;
                            }
                        }
                    }
                }
                count++;

            }

            return listDown;
        }
        public void GetImagesLinkFromUrl2()
        {
            StreamWriter sw = new StreamWriter("data.txt", false);

            //List<ItemDown> listDown = new List<ItemDown>();

            //create a new folder
            string targetfolder = "";
            string directory;

            //tạo vị trí lưu
            string foldername = CreateFolderName(url);
            targetfolder = savepath + "\\" + foldername; ;
            System.IO.Directory.CreateDirectory(targetfolder);

            //lay cac duong link gallery
            WebRequest webRequest = WebRequest.Create(url);
            WebResponse webresponse = webRequest.GetResponse();
            StreamReader inStream = new StreamReader(webresponse.GetResponseStream());
            String htmlstring = inStream.ReadToEnd();
            Document doc = NSoupClient.ParseBodyFragment(htmlstring);

            List<string> listTemp = new List<string>();
            Elements Links = doc.Select("a");

            string imagedownloadlink = "";
            //lay ra link trang chua anh, tu link nay moilay ra duoc anh, rat vai
            foreach (Element link in Links)
            {
                if (link.Parent.ClassName().Equals("ngg-gallery-thumbnail"))
                {
                    string imagelink = link.Attr("href");
                    imagelink = "http://www.bcfakes.com" + imagelink;

                    WebRequest webRequest3 = WebRequest.Create(imagelink);
                    WebResponse webresponse3 = webRequest3.GetResponse();
                    StreamReader inStream3 = new StreamReader(webresponse3.GetResponseStream());
                    String htmlstring3 = inStream3.ReadToEnd();
                    Document doc3 = NSoupClient.ParseBodyFragment(htmlstring3);

                    Elements linkdownFinals = doc3.Select("a");

                    foreach (Element linkdownFinal in linkdownFinals)
                    {
                        if (linkdownFinal.Parent.ClassName().Equals("pic"))
                        {
                            imagedownloadlink = linkdownFinal.Attr("href");
                            break;
                        }
                    }
                    
                }
                
                if (!imagedownloadlink.Equals("")) break;
            }

            string temptemp = imagedownloadlink.Substring(imagedownloadlink.LastIndexOf("_")+1, imagedownloadlink.LastIndexOf(".") - imagedownloadlink.LastIndexOf("_")-1);
            int SoChuSo0 = temptemp.Length;
            int lastindex = int.Parse(temptemp);
            string LinkdataDown = imagedownloadlink.Substring(0, imagedownloadlink.LastIndexOf("_")+1);

            

            for (int i = 1; i <= lastindex; i++)
            {
                string StringAdd = "";
                int Dem = i.ToString().Length;
                for(int k=Dem;k<SoChuSo0;k++)
                {
                    StringAdd = StringAdd + "0";
                }
                string linkdataaddtodownload = "";

                linkdataaddtodownload = LinkdataDown + StringAdd + i + ".jpg";
                

                int int1 = linkdataaddtodownload.LastIndexOf("/") + 1;
                int int2 = linkdataaddtodownload.LastIndexOf(".");
                directory = targetfolder + @"\" + linkdataaddtodownload.Substring(int1, int2 - int1) + ".jpg";

                sw.WriteLine(i + "#" + directory + "#" + linkdataaddtodownload + "#" + 0 + "#waiting");

                //ItemDown temp = new ItemDown(i, directory, linkdataaddtodownload,0, "waiting");
                //listDown.Add(temp);
            }
            //return listDown;
            sw.Close();
        }
     
    }
}
