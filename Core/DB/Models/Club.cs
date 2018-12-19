using System;
using System.Collections.Generic;

namespace Core.DB.Models
{
    public partial class Club
    {
        public Club()
        {
            Players = new HashSet<Player>();
        }

        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid? RegionId { get; set; }

        public virtual ICollection<Player> Players { get; set; }
    }
}
