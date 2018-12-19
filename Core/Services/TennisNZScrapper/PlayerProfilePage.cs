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
    public class PlayerProfilePage
    {
       
        public Player GetPlayerProfile(string playerId)
        {
            var htmlDoc = TennisNzSite.GetTennisNZPage($"ResultsHistoryList.asp?pID={playerId}&gtID=2&CP=GradingList");
            var fullName = htmlDoc.DocumentNode.SelectSingleNode("//td[@class='subhdg']").InnerText;

            return new Player {Name = fullName };
        }

        public List<Match> GetPlayerResults(string playerId)
        {
            var htmlDoc = TennisNzSite.GetTennisNZPage($"ResultsHistoryList.asp?pID={playerId}&gtID=2&CP=GradingList");
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

       
       
    }


}
