using System;
using System.Collections.Generic;
using System.Linq;

namespace WebImageDownloader
{
    public class ItemHtmlSelect
    {
        public string link { get; set; }
        public string status { get; set; }

        public ItemHtmlSelect(string _link, string _status)
        {
            link = _link;
            status = _status;
        }
    }
}
