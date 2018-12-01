using System;

namespace DataAccess.DapperModels
{
    public class Round : BaseModel
    {
        public Guid GameId { get; set; }
        public int RoundNumber { get; set; }
        public string Deck { get; set; }
    }
}
