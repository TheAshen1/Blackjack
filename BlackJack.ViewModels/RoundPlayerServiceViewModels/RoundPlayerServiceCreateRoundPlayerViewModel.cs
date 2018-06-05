using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.ViewModels.RoundPlayerServiceViewModels
{
    public class RoundPlayerServiceCreateRoundPlayerViewModel
    {
        public string RoundId { get; set; }

        public string PlayerId { get; set; }

        public List<String> PlayerCards { get; set; }
    }
}
