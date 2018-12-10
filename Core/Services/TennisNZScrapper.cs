using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Xml.Linq;
using Core.Models;
using Core.Utilities;


namespace Core.Services
{
    public class TennisNZScrapper
    {
        private static string _baseUrl = "https://tennis.org.nz/";

        public Player GetPlayerProfile(string playerId)
        {
            var htmlDoc = Html.GetDocument(new Uri($"{_baseUrl}ResultsHistoryList.asp?pID={playerId}&gtID=2&CP=GradingList"));
            var fullName = htmlDoc.DocumentNode.SelectSingleNode("//td[@class='subhdg']").InnerText;

            return new Player {Name = fullName };
        }
    }
}
