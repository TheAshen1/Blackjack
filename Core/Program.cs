using DataAccess.Models;
using BlackJack.BusinessLogic.GameLogic;
using DataAccess;
using System;
using System.Linq;

namespace Core
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Begin!");

            BlackJackContext context = new BlackJackContext();
          
            var deck = new DeckLogic();

            Console.WriteLine(deck.Stringify());

            var Players = new Player[]
            {
                new Player
                {
                    Id = Guid.NewGuid(),
                    Name = "Dealer",
                    IsBot = true
                },
                new Player
                {
                    Id = Guid.NewGuid(),
                    Name = "Player",
                    IsBot = true
                }
            };

            var Game = new Game
            {
                Id = Guid.NewGuid(),
                Players = Players,
                Start = DateTime.Now,
                End = DateTime.Now.AddMinutes(5)
            };

            var Rounds = new Round[]
            {
                new Round
                {
                    Id = Guid.NewGuid(),
                    Game = Game,
                    Deck = (new DeckLogic()).Stringify()
                },
                new Round
                {
                    Id = Guid.NewGuid(),
                    Game = Game,
                    Deck =  (new DeckLogic()).Stringify()
                }
            };
            var RoundPlayers = new RoundPlayer[]
            {
                new RoundPlayer
                {
                    Id = Guid.NewGuid(),
                    Round = Rounds[0],
                    Player = Players[0],
                    Cards = "TEN of CLUBS, FIVE of HEARTS"
                },
                new RoundPlayer
                {
                    Id = Guid.NewGuid(),
                    Round = Rounds[0],
                    Player = Players[0],
                    Cards = "EIGHT of PIKE, TEN of HEARTS"
                },
                new RoundPlayer
                {
                    Id = Guid.NewGuid(),
                    Round = Rounds[1],
                    Player = Players[1],
                    Cards = "TEN of PIKE, ACE of DIAMONDS"
                },
                new RoundPlayer
                {
                    Id = Guid.NewGuid(),
                    Round = Rounds[1],
                    Player = Players[1],
                    Cards = "FIVE of DIAMONDS, ACE of CLUBS"
                }
            };

            context.RoundPlayers.AddRange(RoundPlayers);

            context.SaveChanges();

            Console.WriteLine("Saved!");



            var query = from roundPlayer in context.RoundPlayers.Local
                        select roundPlayer;

            Console.WriteLine("Query result: " + query.Count());
            foreach (var roundPlayer in query)
            {
                Console.WriteLine("Id: " + roundPlayer.Id + " RoundId: " + roundPlayer.RoundId + " PlayerId: " + roundPlayer.PlayerId + "Cards: " + roundPlayer.Cards);
            }
            Console.WriteLine("Done!");


            /**/

            //var gameLogic = new GameLogic();
            //Console.WriteLine("Enter your name");
            //var name = Console.ReadLine();
            //Console.WriteLine("Enter number of players(from 2 up to 5)");

            //var tmpString = Console.ReadLine();
            //int numberOfPlayers = 2;
            //if (Int32.TryParse(tmpString, out numberOfPlayers))
            //{
            //    gameLogic.StartNewGame(name, numberOfPlayers);
            //}
            //else
            //{
            //    Console.WriteLine("Does not compute!");
            //}

        }


    }
}
