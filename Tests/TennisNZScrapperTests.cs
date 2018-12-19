using System.Collections.Generic;
using Core.Models;
using Core.Services.TennisNzScrapper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace TennisNzScrapperTests
{
    [TestClass]
    public class TennisNzScrapperTests
    {
        private string _playerIdAnthonyMarshall = "104869";

        [TestMethod]
        public void Can_Get_PlayerProfile()
        {
            var scrapper = new Core.Services.TennisNzScrapper.PlayerProfilePage();

            var player =  scrapper.GetPlayerProfile(_playerIdAnthonyMarshall);

            Assert.IsNotNull(player);
            Assert.IsTrue(player.Name=="Anthony Marshall");
        }

        [TestMethod]
        public void Can_Parse_PlayerResults()
        {
            var scrapper = new Core.Services.TennisNzScrapper.PlayerProfilePage();

            List<Match> matches = scrapper.GetPlayerResults(_playerIdAnthonyMarshall);

            Assert.IsNotNull(matches);
            Assert.IsTrue(matches.Count > 0);
        }

        [TestMethod]
        public void Can_Get_All_Players()
        {
            List<Player> players = new GradingListPage().GetAllPlayers();
            
            Assert.IsNotNull(players);
            Assert.IsTrue(players.Count > 10);
        }

    }
}
