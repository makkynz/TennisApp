using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Core.Extensions;
using Core.Models;
using Core.Utilities;
using HtmlAgilityPack;

namespace Core.Services.TennisNzScrapper
{
    public class GradingListPage
    {


        private string _cachePlayersFile =
            $@"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\PlayersCache.json";


        public List<Player> GetAllPlayers(bool fromCache = true)
        {
            List<Player> tennisNzPlayers = null;

            if (fromCache)
            {
                tennisNzPlayers = FileIO.Read(_cachePlayersFile)?.ToObject<List<Models.Player>>();
            }

            if (tennisNzPlayers != null) return tennisNzPlayers;


            var players = new List<Player>();
            var url = $"GradingList.asp";
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
            var firstPage = TennisNzSite.GetTennisNZPage(url, postData, "POST");
            players.AddRange(ExtractPlayer(firstPage));

            //get all other pages
            var nextPagePlayers = ExtractPlayer2(TennisNzSite.GetTennisNZPage(url, postData.Replace("Search", "Next"), "POST"));
            while (nextPagePlayers.Count > 0)
            {
                players.AddRange(nextPagePlayers);
                nextPagePlayers = ExtractPlayer2(TennisNzSite.GetTennisNZPage(url, postData.Replace("Search", "Next"), "POST"));
            }

            FileIO.Save(_cachePlayersFile, players.ToJson());

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

                        var playerUrl = row.SelectSingleNode("td[3]/a[1]").Attributes["href"].Value;
                        var playerId = playerUrl.Substring(playerUrl.IndexOf("=") + 1,
                            playerUrl.IndexOf("&") - playerUrl.IndexOf("=") - 1);

                        players.Add(new Player
                        {
                            Rank = Convert.ToInt32(rank.GetNumbers()),
                            Grade = row.SelectSingleNode("td[2]").InnerText,
                            Name = row.SelectSingleNode("td[3]").InnerText,
                            PlayerCode = row.SelectSingleNode("td[4]").InnerText,
                            Club = row.SelectSingleNode("td[6]").InnerText,
                            PlayerId = playerId,
                        });
                    }
                }
            }

            return players;
        }

        private static ICollection<Player> ExtractPlayer2(HtmlDocument htmlPage)
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
                    var rank = row.SelectSingleNode("td[2]")?.InnerText;
                    if (rank != null)
                    {

                        var playerUrl = row.SelectSingleNode("td[4]/a[1]")?.Attributes["href"].Value;
                        var playerId = playerUrl?.Substring(playerUrl.IndexOf("=") + 1,
                                           playerUrl.IndexOf("&") - playerUrl.IndexOf("=") - 1) ?? "na";

                        players.Add(new Player
                        {
                            Rank = Convert.ToInt32(rank.GetNumbers()),
                            Grade = row.SelectSingleNode("td[3]").InnerText,
                            Name = row.SelectSingleNode("td[4]").InnerText,
                            PlayerCode = row.SelectSingleNode("td[5]").InnerText,
                            Club = row.SelectSingleNode("td[7]").InnerText,
                            PlayerId = playerId,
                        });
                    }
                }
            }

            return players;
        }
    }


}
