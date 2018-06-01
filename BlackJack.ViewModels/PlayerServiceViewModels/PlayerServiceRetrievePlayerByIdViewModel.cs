using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.ViewModels.PlayerServiceViewModels
{
    class PlayerServiceRetrievePlayerByIdViewModel
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        //public virtual ICollection<Round_Player> Round_Player { get; set; }
    }
}
