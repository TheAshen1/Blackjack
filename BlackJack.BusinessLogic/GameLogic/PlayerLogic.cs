using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogic.GameLogic
{
    public class PlayerLogic
    {
        public string Name;
        public bool IsBot;
        public List<CardLogic> Hand = new List<CardLogic>();
    }
}
