using System;
using System.Collections.Generic;

namespace BlackJack.Corang.Models
{
    public class Player : BaseModel
    {
        public string Name { get; set; }

        public bool IsBot { get; set; }

        public Guid GameId { get; set; }
        public Game Game { get; set; }

        public virtual ICollection<RoundPlayer> PlayerRounds { get; set; }

    }
}
