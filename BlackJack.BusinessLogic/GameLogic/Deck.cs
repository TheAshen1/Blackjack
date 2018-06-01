using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogic.GameLogic
{
    public class Deck
    {
        private List<Card> cards;

        public Deck()
        {
            foreach (Value value in Enum.GetValues(typeof(Value)))
            {
                foreach (Suit suit in Enum.GetValues(typeof(Suit)))
                {
                    cards.Add(new Card(value, suit));
                }
            }

            Shuffle();
        }

        public void Shuffle()
        {
            List<Card> tmpDeck = new List<Card>();
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


        public Card Draw()
        {
            var rand = new Random();
            var x = rand.Next(cards.Count);
            var card = cards[x];
            cards.RemoveAt(x);
            return card;
        }
    }
}
