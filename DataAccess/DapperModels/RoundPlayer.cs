using System;

namespace DataAccess.DapperModels
{
    public class RoundPlayer : BaseModel
    {
        public Guid PlayerId { get; set; }
        public Guid RoundId { get; set; }
        public int Bet { get; set; }
        public string Cards { get; set; }
    }
}
