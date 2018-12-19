using System;
using System.Collections.Generic;

namespace Core.DB.Models
{
    public partial class Player
    {
        public Player()
        {
            MatchPlayer1s = new HashSet<Match>();
            MatchPlayer2s = new HashSet<Match>();
            MatchPlayer3s = new HashSet<Match>();
            MatchPlayer4s = new HashSet<Match>();
        }

        public Guid Id { get; set; }
        public string TennisNzId { get; set; }
        public Guid? ClubId { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int? Rank { get; set; }
        public string SinglesGrading { get; set; }
        public int? SingleGradingPoints { get; set; }
        public string DoubleGrading { get; set; }
        public int? DoubleGradingPoints { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
        public string Woot { get; set; }

        public virtual Club Club { get; set; }
        public virtual ICollection<Match> MatchPlayer1s { get; set; }
        public virtual ICollection<Match> MatchPlayer2s { get; set; }
        public virtual ICollection<Match> MatchPlayer3s { get; set; }
        public virtual ICollection<Match> MatchPlayer4s { get; set; }
    }
}
