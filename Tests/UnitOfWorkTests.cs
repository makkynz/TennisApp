using System.Collections.Generic;
using System.Linq;
using Core.DB.Models;
using Core.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitOfWorkTests
{
    [TestClass]
    public class UnitOfWorkTests
    {
        
        [TestMethod]
        public void Can_Populate_Db()
        {
            var seed = new Core.UnitOfWork.SeedDb();

            seed.BuildFromEmptyDb(true);

            var playerCount = 0;
            using (var db = new TennisAPpContext())
            {
                playerCount = db.Player.Count();
            }
            
            Assert.IsTrue(playerCount > 100);
        }

        

    }
}
