using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using System.Xml.Linq;
using HtmlAgilityPack;


namespace Core.Utilities
{
    public class Html
    {
        public static string Get(Uri uri)
        {
            string html;
            using (var client = new WebClient())
            {
                html = client.DownloadString(uri.AbsoluteUri);
            }
            return html;
        }

        public static HtmlDocument GetDocument(string url)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(Get(new Uri(url)));
            return doc;
        }

        
    }
}

