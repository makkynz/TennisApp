using System;
using System.Collections.Generic;

namespace Core.DB.Models
{
    public partial class Match
    {
        public Guid Id { get; set; }
        public DateTime? Date { get; set; }
        public Guid? Player1Id { get; set; }
        public Guid? Player2Id { get; set; }
        public Guid? Player3Id { get; set; }
        public Guid? Player4Id { get; set; }
        public string Result { get; set; }
        public Guid? ClubVenueId { get; set; }

        public virtual Player Player1 { get; set; }
        public virtual Player Player2 { get; set; }
        public virtual Player Player3 { get; set; }
        public virtual Player Player4 { get; set; }
    }
}
