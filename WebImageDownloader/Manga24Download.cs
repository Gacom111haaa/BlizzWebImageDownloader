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
    class Manga24Download : GenaralDownload
    {
        public Manga24Download(string path, string name, string url1)
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

            //lay ten truyen -> tao duong dan cho gon
            string mangaName = url.Substring(url.LastIndexOf("/")+1).Replace(".html", "");

            StreamReader inStream;
            WebRequest webRequest;
            WebResponse webresponse;
            webRequest = WebRequest.Create(url);
            webresponse = webRequest.GetResponse();
            inStream = new StreamReader(webresponse.GetResponseStream());
            String htmlstring = inStream.ReadToEnd();
            Document doc = NSoupClient.ParseBodyFragment(htmlstring);

            List<string> listTemp = new List<string>();

            //Buoc 1: tu link ten truyen lay ra duong link cua tung chap
            Elements Links = doc.Select("a");
            foreach (Element link in Links)
            {
                if ("item-even".Equals(link.Parent.Parent.ClassName()) || "item-odd".Equals(link.Parent.Parent.ClassName()))
                {
                    string imagelink = link.SiblingElements.Text + "###" + "http://manga24h.com/" + link.Attr("href");                 
                    listTemp.Add(imagelink);
                }
            }

            //Buoc 2: tu link moi chap truyen dua ra cac link anh cua tung chap truyen
            foreach (string item in listTemp)
            {
                //tạo vị trí lưu
                string linkChap = item.Substring(item.LastIndexOf("###")+3);
                string countChap = item.Substring(0, item.IndexOf("###")).Replace(@"\", "").Replace(@"/", "").Replace(@":", "")
                    .Replace(@"*", "").Replace(@"?", "").Replace("\"", "").Replace(@"<", "").Replace(@">", "").Replace(@"|", "");
                string foldername = CreateFolderName(linkChap);
                targetfolder = savepath + "\\" + mangaName + "\\" + countChap;// +"_" + foldername;
                System.IO.Directory.CreateDirectory(targetfolder);

                //down
                WebRequest webRequest2 = WebRequest.Create(linkChap);
                WebResponse webresponse2 = webRequest2.GetResponse();
                StreamReader inStream2 = new StreamReader(webresponse2.GetResponseStream());
                String htmlstring2 = inStream2.ReadToEnd();

                int Start = htmlstring2.IndexOf("data='") + 6;
                int Stop = htmlstring2.IndexOf("images = new Array();");

                string datacontent = htmlstring2.Substring(Start, Stop - Start);
                datacontent = datacontent.Substring(0, datacontent.LastIndexOf("';"));
                string[] linkdown = datacontent.Split('|');
                //Document doc2 = NSoupClient.ParseBodyFragment(htmlstring2);

                //Elements linkdown = doc2.Select("img");

                int i = 0;

                //foreach (Element link in linkdown)
                foreach (string link in linkdown)
                {
                    //if (link.Parent.ClassName().Equals("view2"))
                    //{
                    string imagelink = link.Replace(";","").Replace("\r","").Replace("\n","").Replace("\t",""); // link.Attr("src");
                        
                        if (i < 10)
                            directory = targetfolder + "\\" + savename + "00" + i + ".jpg";
                        else if (i >= 10 && i < 100)
                            directory = targetfolder + "\\" + savename + "0" + i + ".jpg";
                        else directory = targetfolder + "\\" + savename + i + ".jpg";

                        ItemDown temp = new ItemDown(i,directory, imagelink, 0,"waiting");
                        
                        listDown.Add(temp);
                        i++;
                    //}
                }

            }
            return listDown;
        }

        public override void GetImagesLinkFromUrl2()
        {
            //Tao file luu lai qua trinh load 
            StreamWriter sw = new StreamWriter("data.txt", false);

            //List<ItemDown> listDown = new List<ItemDown>();

            //create a new folder
            string targetfolder = "";
            string directory;

            //lay ten truyen -> tao duong dan cho gon
            string mangaName = url.Substring(url.LastIndexOf("/") + 1).Replace(".html", "");

            StreamReader inStream;
            WebRequest webRequest;
            WebResponse webresponse;
            webRequest = WebRequest.Create(url);
            webresponse = webRequest.GetResponse();
            inStream = new StreamReader(webresponse.GetResponseStream());
            String htmlstring = inStream.ReadToEnd();
            Document doc = NSoupClient.ParseBodyFragment(htmlstring);

            List<string> listTemp = new List<string>();

            //Buoc 1: tu link ten truyen lay ra duong link cua tung chap
            Elements Links = doc.Select("a");
            foreach (Element link in Links)
            {
                if ("item-even".Equals(link.Parent.Parent.ClassName()) || "item-odd".Equals(link.Parent.Parent.ClassName()))
                {
                    string imagelink = link.SiblingElements.Text + "###" + "http://manga24h.com/" + link.Attr("href");
                    listTemp.Add(imagelink);
                }
            }

            //Buoc 2: tu link moi chap truyen dua ra cac link anh cua tung chap truyen
            foreach (string item in listTemp)
            {
                //tạo vị trí lưu
                string linkChap = item.Substring(item.LastIndexOf("###") + 3);
                string countChap = item.Substring(0, item.IndexOf("###")).Replace(@"\", "").Replace(@"/", "").Replace(@":", "")
                    .Replace(@"*", "").Replace(@"?", "").Replace("\"", "").Replace(@"<", "").Replace(@">", "").Replace(@"|", "");
                string foldername = CreateFolderName(linkChap);
                targetfolder = savepath + "\\" + mangaName + "\\" + countChap;// +"_" + foldername;
                System.IO.Directory.CreateDirectory(targetfolder);

                //down
                WebRequest webRequest2 = WebRequest.Create(linkChap);
                WebResponse webresponse2 = webRequest2.GetResponse();
                StreamReader inStream2 = new StreamReader(webresponse2.GetResponseStream());
                String htmlstring2 = inStream2.ReadToEnd();

                int Start = htmlstring2.IndexOf("data='") + 6;
                int Stop = htmlstring2.IndexOf("images = new Array();");

                string datacontent = htmlstring2.Substring(Start, Stop - Start);
                datacontent = datacontent.Substring(0, datacontent.LastIndexOf("';"));
                string[] linkdown = datacontent.Split('|');
                //Document doc2 = NSoupClient.ParseBodyFragment(htmlstring2);

                //Elements linkdown = doc2.Select("img");

                int i = 0;

                //foreach (Element link in linkdown)
                foreach (string link in linkdown)
                {
                    //if (link.Parent.ClassName().Equals("view2"))
                    //{
                    string imagelink = link.Replace(";", "").Replace("\r", "").Replace("\n", "").Replace("\t", ""); // link.Attr("src");

                    if (i < 10)
                        directory = targetfolder + "\\" + savename + "00" + i + ".jpg";
                    else if (i >= 10 && i < 100)
                        directory = targetfolder + "\\" + savename + "0" + i + ".jpg";
                    else directory = targetfolder + "\\" + savename + i + ".jpg";

                    //ItemDown temp = new ItemDown(i, directory, imagelink, 0, "waiting");
                    sw.WriteLine(i + "#" + directory + "#" + imagelink + "#" + 0 + "#waiting");
                    //listDown.Add(temp);
                    i++;
                    //}
                }

            }
            sw.Close();
        }

             
    }
}
