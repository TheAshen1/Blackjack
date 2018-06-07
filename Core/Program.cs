
using BlackJack.BusinessLogic.GameLogic;
using BlackJack.BusinessLogic.Services;
using DataAccess;
using DataAccess.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Core
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Begin!");

            BlackJackContext context = new BlackJackContext();

            //context.Rounds.Load();
            //var queryRounds = from round in context.Rounds.Local
            //                  select round;
            //var someRound = queryRounds.First();

            //context.Players.Load();
            //var queryPlayers = from player in context.Players.Local
            //                   select player;
            //var somePlayer = queryPlayers.First();

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
                    Game = Game                   
                },
                new Round
                {
                    Id = Guid.NewGuid(),
                    Game = Game
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



            var query = from roundPlayer in context.RoundPlayers
                        select roundPlayer;

            Console.WriteLine("Query result: " + query.Count());
            foreach (var roundPlayer in query)
            {
                Console.WriteLine("Id: " + roundPlayer.Id + " RoundId: " + roundPlayer.RoundId + " PlayerId: " + roundPlayer.PlayerId + "Cards: " + roundPlayer.Cards);
            }
            Console.WriteLine("Done!");

            /**/

            //Console.WriteLine("Begin!");

            //var testService = new PlayerService();

            //var players = new Player[]
            //{
            //    new Player()
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "SomeUser"
            //    }
            //};

            //var result = testService.All();

            //Console.WriteLine("Query result: " + result.Count());
            //foreach (var player in result)
            //{
            //    Console.WriteLine(player.Name);
            //}
            //Console.WriteLine("Done!");

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
