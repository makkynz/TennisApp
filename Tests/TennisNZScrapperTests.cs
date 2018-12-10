using Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests
{
    [TestClass]
    public class HtmlTests
    {
        [TestMethod]
        public void Can_Get_Data_From_TennisNZ()
        {
            var scrapper = new Core.Services.TennisNZScrapper();

            var player =  scrapper.GetPlayerProfile(playerId: "104869");

            Assert.IsNotNull(player);
            Assert.IsTrue(player.Name=="Anthony Marshall");
        }
    }
}
