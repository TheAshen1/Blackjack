using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.ViewModels.PlayerServiceViewModels
{
    public class PlayerServiceCreatePlayerViewModel
    {
        public string Name { get; set; }
        public string GameId { get; set; }
        public bool IsBot { get; set; }
        public int Chips { get; set; }
    }
}
