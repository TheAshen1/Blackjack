using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.ViewModels.PlayerServiceViewModels
{
    public class PlayerRoundServiceViewModel
    {
        public Guid Id { get; set; }

        public Guid GameId { get; set; }

        public Guid PlayerId { get; set; }

        //public string Name { get; set; }

        //public bool IsBot { get; set; }

        public List<String> PlayerCards { get; set; }
    }
}
