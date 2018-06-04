using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogic.GameLogic
{
    public class DeckLogic
    {
        private List<CardLogic> cards = new List<CardLogic>();

        public DeckLogic()
        {
            foreach (ValueLogic value in Enum.GetValues(typeof(ValueLogic)))
            {
                foreach (SuitLogic suit in Enum.GetValues(typeof(SuitLogic)))
                {
                    cards.Add(new CardLogic(value, suit));
                }
            }

            Shuffle();
        }

        public void Shuffle()
        {
            List<CardLogic> tmpDeck = new List<CardLogic>();
            var rand = new Random();


            var randomIndex = 0;
            var deckSize = cards.Count;

            for (int i = 0; i < deckSize; i++)
            {

                randomIndex = rand.Next((cards.Count));
                tmpDeck.Add(cards[randomIndex]);

                cards.RemoveAt(randomIndex);
            }
           cards = tmpDeck;
        }


        public CardLogic Draw()
        {
            var rand = new Random();
            var x = rand.Next(cards.Count);
            var card = cards[x];
            cards.RemoveAt(x);
            return card;
        }
    }
}
