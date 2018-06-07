using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.ViewModels.GameLogicViewModels
{
    public class GameLogicViewModel
    {
        public string GameId { get; set; }
        public string CurrentRoundId { get; set; }

        public List<PlayerLogicViewModel> Players { get; set; }
    }
}
