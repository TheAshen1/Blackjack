using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.ViewModels.GameLogicViewModels
{
    public class PlayerLogicViewModel
    {
        public string Id { get; set; }
        public string CurrentRoundPlayerId { get; set; }
        public string Name { get; set; }
        public bool IsBot { get; set; }
        public List<string> Cards { get; set; }

    }
}
