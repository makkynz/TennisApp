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
           
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AllowAutoRedirect = true;
            request.CookieContainer = cookies;
            request.Method = "POST";
          
          
            var postData = "fPlayerSurname=" +
                           "&fSex=M" +
                           "&fGradingType=2" +
                           "&fGradeCd=" +
                           "&frd=" +
                           "&fClub=" +
                           "&fAgegroup=" +
                           "&fAgegroupDate=30+Nov+2018" +
                           "&MyStuff=TopDog+Top+Dog+Yardstick" +
                           "&MySubmitAction=Search" +
                           "&CallingPage=GradingList.asp" +
                           "&GradingListIsSubmitted=Yes";

            var postDate2 = "fPlayerSurname=" +
                            "&fSex=M" +
                            "&fGradingType=2" +
                            "&fGradeCd=" +
                            "&frd=" +
                            "&fClub=5" +
                            "&fAgegroup=" +
                            "&fAgegroupDate=30+Nov+2018" +
                            "&MyStuff=TopDog+Top+Dog+Yardstick" +
                            "&MySubmitAction=Next" +
                            "&CallingPage=GradingList.asp" +
                            "&GradingListIsSubmitted=Yes";

            var data = Encoding.ASCII.GetBytes(postData);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
           
            string html = "";
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                html = readStream.ReadToEnd();

                response.Close();
                readStream.Close();
            }
            return html;
        }


        public static string Post(Uri uri, String postData)
        {

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri);
            request.AllowAutoRedirect = true;
            request.CookieContainer = cookies;
            request.Method = "POST";

            var data = Encoding.ASCII.GetBytes(postData);
            request.ContentType = "application/x-www-form-urlencoded";
            request.ContentLength = data.Length;
            using (var stream = request.GetRequestStream())
            {
                stream.Write(data, 0, data.Length);
            }

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            string html = "";
            if (response.StatusCode == HttpStatusCode.OK)
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                html = readStream.ReadToEnd();

                response.Close();
                readStream.Close();
            }
            return html;
        }

        public static HtmlDocument GetDocument(string url)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(Get(new Uri(url)));
            return doc;
        }

        public static HtmlDocument GetDocument(string url, string postData, string httpMethod = "GET")
        {
            var doc = new HtmlDocument();
            switch (httpMethod.ToUpper())
            {
                case "POST":
                    doc.LoadHtml(Post(new Uri(url), postData));
                    break;

               default: doc.LoadHtml(Get(new Uri(url)));
                    break;


            }
           
            
            return doc;
        }

        public  static CookieContainer cookies = new CookieContainer();

        
    }
}

