using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.ViewModels.PlayerServiceViewModels
{
    public class PlayerServiceViewModel
    {
        public string Id { get; set; }
        public string GameId { get; set; }

        public string Name { get; set; }

        public bool IsBot { get; set; }

    }
}
