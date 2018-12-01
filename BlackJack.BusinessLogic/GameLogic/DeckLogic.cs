using System;
using System.Collections.Generic;

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
            var x = rand.Next(_cards.Count-1);
            var card = _cards[x];
            _cards.RemoveAt(x);
            return card;
        }

        public string Stringify()
        {
            var json = "[";
            foreach (var card in _cards)
            {
                json += card.ToString() + ",";
            }
            json = json.Substring(0, json.Length - 1);
            json += "]";
            return json;
        }
        public static string Stringify(IEnumerable<CardLogic> cards)
        {
            var json = "[";
            foreach (var card in cards)
            {
                json += card.ToString() + ",";
            }
            json = json.Substring(0, json.Length - 1);
            json += "]";
            return json;
        }
    }
}
