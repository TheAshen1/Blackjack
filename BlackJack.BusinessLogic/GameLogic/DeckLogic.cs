using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace BlackJack.BusinessLogic.GameLogic
{
    public class DeckLogic
    {
        private List<CardLogic> _cards = new List<CardLogic>();

        public DeckLogic()
        {
            foreach (ValueLogic value in Enum.GetValues(typeof(ValueLogic)))
            {
                foreach (SuitLogic suit in Enum.GetValues(typeof(SuitLogic)))
                {
                    _cards.Add(new CardLogic(value, suit));
                }
            }

            Shuffle();
        }
        public DeckLogic(List<CardLogic> cards)
        {
            _cards = cards; 
            Shuffle();
        }

        public void Shuffle()
        {
            List<CardLogic> tmpDeck = new List<CardLogic>();
            var rand = new Random();


            var randomIndex = 0;
            var deckSize = _cards.Count;

            for (int i = 0; i < deckSize; i++)
            {

                randomIndex = rand.Next((_cards.Count));
                tmpDeck.Add(_cards[randomIndex]);

                _cards.RemoveAt(randomIndex);
            }
            _cards = tmpDeck;
        }


        public CardLogic Draw()
        {
            var rand = new Random();
            var x = rand.Next(_cards.Count);
            var card = _cards[x];
            _cards.RemoveAt(x);
            return card;
        }

        public string Stringify()
        {
            var json = new JavaScriptSerializer().Serialize(_cards);
            return json;
        }
    }
}
