using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WebImageDownloader
{
    class ItemDown
    {
        public int ID { set; get;}
        public string Name { set;get;}
        public string savepath { set; get; }
        public string linkdown { set; get; }
        public int percentage { set; get;}
        public string status { set; get; }

        public ItemDown(int _iD,string _savepath, string _linkdown,int _percentage, string _status)
        {
            ID = _iD;
            savepath = _savepath;
            linkdown = _linkdown;
            status = _status;
            percentage = _percentage;
        }

        
    }
}
