using System;
using System.Collections.Generic;

namespace BlackJack.Corang.Models
{
    public class Round : BaseModel
    {
        public Guid GameId { get; set; }
        public Game Game { get; set; }

        public string Deck { get; set; }

        public virtual ICollection<RoundPlayer> RoundPlayers { get; set; }

    }
}
