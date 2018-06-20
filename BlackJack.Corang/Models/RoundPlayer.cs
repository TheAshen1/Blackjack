using System;

namespace BlackJack.Corang.Models
{
    public class RoundPlayer : BaseModel
    {
        public Guid PlayerId { get; set; }
        public Player Player { get; set; }
        public Guid RoundId { get; set; }
        public Round Round { get; set; }

        public string Cards { get; set; }

    }
}
