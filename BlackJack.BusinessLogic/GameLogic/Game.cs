using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogic.GameLogic
{
    class Game
    {
        public void StartNewGame(string userName, int playerCount = 2)
        {
            if (playerCount > 5)
            {
                Console.WriteLine("Player count is limited to 5!");
                return;
            }
            if (playerCount < 2)
            {
                Console.WriteLine("Minimum amount of players is 2!");
                return;
            }

            Console.WriteLine("Welcome to BlackJack!");

            var RAND = new Random();
            var DECK = new Deck();

            var Dealer = new Player()
            {
                Name = "Dealer"
            };

            var Players = new List<Player>() {
                new Player()
                {
                    Name =userName
                }
            };
            var listOfNames = new List<string>()
            {
                "James",
                "John",
                "Robert",
                "Michael",
                "William",
                "David",
                "Richard",
                "Charles",
                "Joseph",
                "Thomas",
                "Christopher",
                "Daniel",
                "Paul",
                "Mark",
                "Donald",
            };

            for (var i = 2; i < playerCount; i++)
            {
                var tmpIndex = RAND.Next(listOfNames.Count);
                Players.Add(new Player()
                {
                    Name = listOfNames[tmpIndex]
                });
                listOfNames.RemoveAt(tmpIndex);
            }

            /*actual start of the game*/

            while (true) // one cycle means one round
            {
                //giving cards
                foreach (var player in Players)
                {
                    player.Hand.Add(DECK.Draw());
                    player.Hand.Add(DECK.Draw());
                }
                Dealer.Hand.Add(DECK.Draw());
                Dealer.Hand.Add(DECK.Draw());

                //players choosing to hit or stand
                foreach (var player in Players)
                {
                    while (CountScore(player) < 21)
                    {
                        if (WantToDrawAnoterCard())
                        {
                            player.Hand.Add(DECK.Draw());
                        }
                    }                 
                }

                //dealer is picking 
                while (CountScore(Dealer) < 17)
                {
                    Dealer.Hand.Add(DECK.Draw());
                }


                //Choose a winner
                var maxScore = CountScore(Dealer);
                var winners = new List<Player>();

                foreach(var player in Players)
                {

                }

                if (!ContinueGame()) return;
            }
        }


        public bool WantToDrawAnoterCard()
        {
            Console.WriteLine("Would you like to draw another card?");
            var input = Console.ReadLine();
            switch (input) {
                case "y": return true; 
                case "n": return false; 
                default:  return false;
            }
                
        }

        public bool ContinueGame()
        {
            Console.WriteLine("Would you like to move to the next round?");
            var input = Console.ReadLine();
            switch (input)
            {
                case "y": return true;
                case "n": return false;
                default: return false;
            }

        }

        public int CountScore(Player player)
        {
            var Score = 0;
            var aces = 0;
            foreach (var Card in player.Hand)
            {
                if(Card.value != Value.ACE)
                {
                    Score += (int) Card.value;
                }
                else
                {
                    aces++;
                }
            }
            for(var i=0; i < aces; i++)
            {
                if (Score <= 21 - (aces + Score))
                {
                    Score += 11;
                }
                else
                {
                    Score++;
                }
            }
           
            return Score;
        }
    }
}
