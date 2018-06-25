using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.ViewModels.RoundServiceViewModels
{
    public class RoundServiceCreateRoundViewModel
    {
        public string GameId { get; set; }
        public int RoundNumber { get; set; }
        public string Deck { get; set; }
    }
}
