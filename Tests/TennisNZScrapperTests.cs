using System.Collections.Generic;
using Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class TennisNZScrapperTests
    {
        private string _playerIdAnthonyMarshall = "104869";

        [TestMethod]
        public void Can_Get_PlayerProfile()
        {
            var scrapper = new Core.Services.TennisNZScrapper();

            var player =  scrapper.GetPlayerProfile(_playerIdAnthonyMarshall);

            Assert.IsNotNull(player);
            Assert.IsTrue(player.Name=="Anthony Marshall");
        }

        [TestMethod]
        public void Can_Parse_PlayerResults()
        {
            var scrapper = new Core.Services.TennisNZScrapper();

            List<Match> matches = scrapper.GetPlayerResults(_playerIdAnthonyMarshall);

            Assert.IsNotNull(matches);
            Assert.IsTrue(matches.Count > 0);
        }

        [TestMethod]
        public void Can_Get_All_Players()
        {
            var scrapper = new Core.Services.TennisNZScrapper();

            List<Player> players = scrapper.GetAllPlayers();
            
            Assert.IsNotNull(players);
            Assert.IsTrue(players.Count > 10);
        }

    }
}
