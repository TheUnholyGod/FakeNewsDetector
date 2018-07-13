using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace FakeNewsDetectorApp
{
    static class SourceGetter
    {
        public static string GetSource(string _url)
        {
            WebClient wc = new WebClient();
            //wc.DownloadFile();
            return "";
        }
    }
}
