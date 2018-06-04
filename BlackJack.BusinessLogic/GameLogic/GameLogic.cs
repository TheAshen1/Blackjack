using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlackJack.BusinessLogic.GameLogic
{
    public class GameLogic
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
            var DECK = new DeckLogic();

            var Dealer = new PlayerLogic()
            {
                Name = "Dealer",
                IsBot = true
            };

            var Players = new List<PlayerLogic>() {
                new PlayerLogic()
                {
                    Name = userName,
                    IsBot = false
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

            foreach(var name in listOfNames)
            {
                if(name == userName)
                {
                    listOfNames.Remove(name);
                }
            }

            for (var i = 2; i < playerCount; i++)
            {
                var tmpIndex = RAND.Next(listOfNames.Count);
                Players.Add(new PlayerLogic()
                {
                    Name = listOfNames[tmpIndex],
                    IsBot = true
                });
                listOfNames.RemoveAt(tmpIndex);
            }

            /*actual start of the game*/

            while (true) // one cycle means one round
            {
                var tmpCard = new CardLogic();
                //giving cards
                foreach (var player in Players)
                {
                    tmpCard = DECK.Draw();
                    Console.WriteLine("Player " + player.Name + " draws " + tmpCard.ToString());
                    player.Hand.Add(tmpCard);

                    tmpCard = DECK.Draw();
                    Console.WriteLine("Player " + player.Name + " draws " + tmpCard.ToString());
                    player.Hand.Add(tmpCard);
                }
                tmpCard = DECK.Draw();
                Console.WriteLine("Dealer" + " draws " + tmpCard.ToString());
                Dealer.Hand.Add(tmpCard);
                tmpCard = DECK.Draw();
                Console.WriteLine("Dealer" + " draws " + tmpCard.ToString());
                Dealer.Hand.Add(tmpCard);


                foreach (var player in Players)
                {
                    //players choice to hit or stand
                    if (!player.IsBot)
                    {
                        while (CountScore(player) < 21 && WantToDrawAnoterCard())
                        {
                            tmpCard = DECK.Draw();
                            Console.WriteLine("Player " + player.Name + " draws " + tmpCard.ToString());
                            player.Hand.Add(tmpCard);                            
                        }
                    }
                    else {
                        while (CountScore(player) < 17)
                        {
                            tmpCard = DECK.Draw();
                            Console.WriteLine("Player " + player.Name + " draws " + tmpCard.ToString());
                            player.Hand.Add(tmpCard);
                        }
                    }
                }

                //dealer is picking 
                while (CountScore(Dealer) < 17)
                {
                    tmpCard = DECK.Draw();
                    Console.WriteLine("Dealer" + " draws " + tmpCard.ToString());
                    Dealer.Hand.Add(tmpCard);
                }


                //Choose a winner
                var maxScore = CountScore(Dealer);
                var winners = new List<PlayerLogic>() { Dealer };
                

                foreach(var player in Players)
                {
                    var playerScore = CountScore(player);

                    if (playerScore > maxScore)
                    {
                        winners.Clear();
                        winners.Add(player);
                    }
                    else if(playerScore == maxScore)
                    {
                        winners.Add(player);
                    }                  
                }


                if(winners.Count == 1)
                {
                    Console.WriteLine("The winner is: ");
                    Console.WriteLine(winners.First().Name);
                }
                else{
                    Console.WriteLine("Winners are: ");
                    foreach(var winner in winners)
                    {
                        Console.WriteLine(winner.Name);
                    }
                 
                }
                //

                if (!ContinueGame()) return;
            }
        }


        public bool WantToDrawAnoterCard()
        {
            Console.WriteLine("Would you like to draw another card? y/n");
            var input = Console.ReadLine();
            switch (input) {
                case "y": return true; 
                case "n": return false; 
                default:  return false;
            }
                
        }

        public bool ContinueGame()
        {
            Console.WriteLine("Would you like to move to the next round? y/n");
            var input = Console.ReadLine();
            switch (input)
            {
                case "y": return true;
                case "n": Console.WriteLine("Goodbye!"); return false;
                default: return false;
            }

        }

        public int CountScore(PlayerLogic player)
        {
            var Score = 0;
            var aces = 0;
            foreach (var Card in player.Hand)
            {
                if(Card.value != ValueLogic.ACE)
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
