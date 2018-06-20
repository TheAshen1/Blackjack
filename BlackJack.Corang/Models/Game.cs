using System;
using System.Collections.Generic;

namespace BlackJack.Corang.Models
{
    public class Game : BaseModel
    {
        public DateTime Start { get; set; }
        public DateTime? End { get; set; }

        public virtual ICollection<Round> Rounds { get; set; }
        public virtual ICollection<Player> Players { get; set; }
    }
}
