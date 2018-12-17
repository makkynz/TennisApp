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
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;


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
           // if (_cachedPages.ContainsKey(url)) return _cachedPages[url];
           return Html.GetDocument(url);
            
            var htmlDoc = Html.GetDocument(url);
            _cachedPages.Add(url, htmlDoc);

            return _cachedPages[url];
        }

        public List<Player> GetAllPlayers()
        {
            var players = new List<Player>();
            var url = $"{_baseUrl}GradingList.asp";
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

            
            
            //get first list of players
            var firstPage = Html.GetDocument(url, postData, "POST");
            players.AddRange(ExtractPlayer(firstPage));

            //get all other pages
            var nextPagePlayers = ExtractPlayer(Html.GetDocument(url, postData.Replace("Search", "Next"), "POST"));
            while (nextPagePlayers.Count > 0)
            {
                players.AddRange(nextPagePlayers);
                nextPagePlayers = ExtractPlayer(Html.GetDocument(url, postData.Replace("Search", "Next"), "POST"));
            }
           
            return players;
        }

        private static ICollection<Player> ExtractPlayer(HtmlDocument htmlPage)
        {
            ICollection<Player> players = new List<Player>();
            var resultsRows = htmlPage.DocumentNode.SelectNodes(
                "//form/table[1]/tr[3]/td[1]/div/table/colgroup[1]/colgroup[1]/colgroup[1]/colgroup[1]/colgroup[1]/colgroup[1]/colgroup[1]/tr");
            if (resultsRows == null)
            {
                resultsRows = htmlPage.DocumentNode.SelectNodes(
                    "//form/table[1]/tr[3]/td[1]/div/table/colgroup[1]/colgroup[1]/colgroup[1]/colgroup[1]/colgroup[1]/colgroup[1]/colgroup[1]/colgroup[1]/tr");

            }

            if (resultsRows != null)
            {
                foreach (HtmlNode row in resultsRows)
                {
                    var rank = row.SelectSingleNode("td[1]")?.InnerText;
                    if (rank != null)
                    {
                         
                        players.Add(new Player
                        {
                            Rank = Convert.ToInt32(rank.GetNumbers()),
                            Grade = row.SelectSingleNode("td[2]").InnerText,
                            Name = row.SelectSingleNode("td[3]").InnerText,
                            PlayerCode = row.SelectSingleNode("td[4]").InnerText,
                            Club = row.SelectSingleNode("td[6]").InnerText,
                        });
                    }
                }
            }

            return players;
        }
    }


}
