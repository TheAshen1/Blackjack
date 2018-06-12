using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogic.GameLogic
{
    public class CardLogic
    {
        public ValueLogic value;
        public SuitLogic suit;

        public CardLogic()
        {
        }

        public CardLogic(ValueLogic value, SuitLogic suit)
        {
            this.value = value;
            this.suit = suit;
        }

        public override string ToString()
        {
            return $"{value.ToString("g")} of {suit.ToString("g")}";
        }
    }
}
