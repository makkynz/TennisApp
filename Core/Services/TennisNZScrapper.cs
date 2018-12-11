using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Xml.Linq;
using Core.Extensions;
using Core.Models;
using Core.Utilities;
using HtmlAgilityPack;


namespace Core.Services
{
    public class TennisNZScrapper
    {
        private static string _baseUrl = "https://tennis.org.nz/";
        private Dictionary<string, HtmlDocument> _cachedPages = new Dictionary<string, HtmlDocument>();

        public Player GetPlayerProfile(string playerId)
        {
            var htmlDoc = GetTennisNZPage($"{_baseUrl}ResultsHistoryList.asp?pID={playerId}&gtID=2&CP=GradingList");
            var fullName = htmlDoc.DocumentNode.SelectSingleNode("//td[@class='subhdg']").InnerText;

            return new Player {Name = fullName };
        }

        public List<Match> GetPlayerResults(string playerId)
        {
            var htmlDoc = GetTennisNZPage($"{_baseUrl}ResultsHistoryList.asp?pID={playerId}&gtID=2&CP=GradingList");
            var resultsRows = htmlDoc.DocumentNode.SelectNodes("//form/table[1]/tr[3]/td[1]/div/table/colgroup[1]/colgroup[1]/colgroup[1]/colgroup[1]/colgroup[1]/colgroup[1]/colgroup[1]/colgroup[1]/tr");

            var matches = new List<Match>();
            foreach (HtmlNode row in resultsRows)
            {
                var matchDate = row.SelectSingleNode("td[1]").InnerText.ToDateTime();
                if (matchDate != null)
                {
                    var playerTwoId = row.SelectSingleNode("td[5]").InnerText;
                    matches.Add(new Match
                    {
                        Date = matchDate.Value,
                        PlayerOneId = playerId,
                        PlayerTwoId = playerTwoId

                    });
                }
            }

            return matches;
        }

        private HtmlDocument GetTennisNZPage(string url)
        {
            if (_cachedPages.ContainsKey(url)) return _cachedPages[url];

            var htmlDoc = Html.GetDocument(url);
            _cachedPages.Add(url, htmlDoc);

            return _cachedPages[url];
        }
    }


}
