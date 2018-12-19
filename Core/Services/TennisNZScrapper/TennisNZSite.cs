using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Xml.Linq;
using Core.Extensions;
using Core.Models;
using Core.Utilities;
using HtmlAgilityPack;



namespace Core.Services.TennisNzScrapper
{
    public class TennisNzSite
    {
        public static string _baseUrl = "https://tennis.org.nz/";
        
        public static HtmlDocument GetTennisNZPage(string page)
        {
           return Html.GetDocument($"{_baseUrl}{page}");
           
        }

        public static HtmlDocument GetTennisNZPage(string page, string postData, string httpMethod = "GET")
        {
            return Html.GetDocument($"{_baseUrl}{page}", postData, httpMethod);

        }
        
       
    }


}
