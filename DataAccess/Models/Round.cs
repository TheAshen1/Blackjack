using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Models
{
    public class Round : BaseModel
    {
        public Guid GameId { get; set; }
        public Game Game { get; set; }

        //public Guid? WinnerId { get; set; }
        //public Player Winner { get; set; }

        public virtual ICollection<RoundPlayer> RoundPlayers { get; set; }

    }
}
