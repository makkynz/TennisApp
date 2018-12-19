using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Core.DB.Models;
using Core.Extensions;
using Core.Services.TennisNzScrapper;
using Core.Utilities;
using Microsoft.EntityFrameworkCore;

namespace Core.UnitOfWork
{
    public class SeedDb
    {
        

        public void BuildFromEmptyDb(bool fromCache = false)
        {
          
            List<Models.Player> tennisNzPlayers = new GradingListPage().GetAllPlayers(fromCache);
           

            using (var db = new TennisAPpContext())
            {
                db.Database.ExecuteSqlCommand("DELETE FROM Player");
                db.Database.ExecuteSqlCommand("DELETE FROM Club");

                var knownClubs = db.Club.ToList();
                foreach (var tennisNzPlayer in tennisNzPlayers)
                {
                    var club = GetOrCreateClub(db, knownClubs, tennisNzPlayer);
                    var player = PopulatePlayer(tennisNzPlayer, club);

                    db.Player.Add(player);

                   
                }

                db.SaveChanges();
            }
        }

        private static Player PopulatePlayer(Models.Player tennisNzPlayer, Club club)
        {
            return new Player
            {
                Id = Guid.NewGuid(),
                Name = tennisNzPlayer.Name,
                Code = tennisNzPlayer.PlayerCode,
                SinglesGrading = tennisNzPlayer.Grade,
                Rank = tennisNzPlayer.Rank,
                Club = club,
                TennisNzId = tennisNzPlayer.PlayerId,
                DateCreated = DateTime.Now,
                DateModified = DateTime.Now
            };
        }

        private Club GetOrCreateClub(TennisAPpContext db, List<Club> knownClubs, Models.Player tennisNzPlayer)
        {
            //check if club is known
            var club = knownClubs.SingleOrDefault(c => c.Name.Equals(tennisNzPlayer.Club));
            if (club == null)
            {
                club = new Club
                {
                    Id = Guid.NewGuid(),
                    Name = tennisNzPlayer.Club,
                };
                db.Club.Add(club);
                knownClubs.Add(club);
            }

            return club;
        }
    }
}
