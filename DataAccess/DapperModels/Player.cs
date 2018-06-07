using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.DapperModels
{
    public class Player : BaseModel
    {
        public string Name { get; set; }

        public bool IsBot { get; set; }

        public Guid GameId { get; set; }
        //public Game Game { get; set; }

        //public virtual ICollection<RoundPlayer> PlayerRounds { get; set; }

    }
}
