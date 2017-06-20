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
    class imagefap: GenaralDownload
    {
        public imagefap(string path, string name, string url1)
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



        //tu link tong ra link forder
        public void GetImagesLinkFromUrl2()
        {
            //List<ItemDown> listDown = new List<ItemDown>();
            StreamWriter sw = new StreamWriter("data.txt", false);

            //create a new folder
            string targetfolder = "";
            string directory;

            string foldername = CreateFolderName(this.url);
            if(foldername.LastIndexOf("?")>0)
                foldername = foldername.Substring(0, foldername.LastIndexOf("?"));
            targetfolder = savepath + "\\" + foldername; ;
            System.IO.Directory.CreateDirectory(targetfolder);

            List<string> listTempA = linkTongRaGa(url);

            foreach (string linkGall in listTempA)
            {
                List<string> listtemp = new List<string>();
                StreamReader inStream;
                WebRequest webRequest;
                WebResponse webresponse;
                webRequest = WebRequest.Create(linkGall);
                webresponse = webRequest.GetResponse();
                inStream = new StreamReader(webresponse.GetResponseStream());
                String htmlstring = inStream.ReadToEnd();
                Document doc = NSoupClient.ParseBodyFragment(htmlstring);

                Elements Links = doc.Select("img");
                foreach (Element link in Links)
                {
                    string imagelink = link.Parent.Attr("href");
                    if (!(imagelink == "") && !(imagelink == "/") && imagelink.IndexOf("photo") > 0)
                    {
                        listtemp.Add("http://www.imagefap.com" + imagelink);
                    }
                }
                int i = 0;

                foreach (string galerylink in listtemp)
                {

                    //lay link anh tu cac link galery
                    try
                    {
                        WebRequest webRequest2 = WebRequest.Create(galerylink);
                        WebResponse webresponse2 = webRequest2.GetResponse();
                        StreamReader inStream2 = new StreamReader(webresponse2.GetResponseStream());
                        String htmlstring2 = inStream2.ReadToEnd();

                        int count1 = htmlstring2.IndexOf("contentUrl") + 14;
                        int count2 = htmlstring2.IndexOf("datePublished") - 7;

                        string imagelink = htmlstring2.Substring(count1, count2 - count1);
                        string savename = imagelink.Substring(imagelink.LastIndexOf("/") + 1);

                        directory = targetfolder + "\\" + savename;
                        sw.WriteLine(i + "#" + directory + "#" + imagelink + "#" + 0 + "#waiting");
                        i++;
                    }

                    catch (Exception ex)
                    {
                    }
                }// het for gallery 
                
            }// het for linkALlgallry
            sw.Close();
        }// end funtion

        private List<string> linkTongRaGa(string url)
        {
            List<string> listtempA = new List<string>();
            StreamReader inStreamA;
            WebRequest webRequestA;
            WebResponse webresponseA;
            webRequestA = WebRequest.Create(url);
            webresponseA = webRequestA.GetResponse();
            inStreamA = new StreamReader(webresponseA.GetResponseStream());
            String htmlstringA = inStreamA.ReadToEnd();
            Document docA = NSoupClient.ParseBodyFragment(htmlstringA);

            Element DIV = docA.GetElementById("gallery");
            Node LinkNode = DIV.GetChildNode(1).GetChildNode(0);

            listtempA.Add(url);

            foreach (Node linkNode in LinkNode.ChildNodes)
            {
                if (linkNode.Attr("class") == "link3" && !(linkNode.ToString().IndexOf("next") > 0))
                {
                    listtempA.Add(url + "/" + linkNode.Attr("href"));
                }
            }

            return listtempA;
        }
    }
}
