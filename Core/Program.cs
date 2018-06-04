
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

namespace Core
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Begin!");

            //BlackJackContext context = new BlackJackContext();

            //var players = new Player[]
            //{
            //    new Player()
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "another user",
            //        IsBot = false
            //    },
            //    new Player()
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "someBot",
            //        IsBot = true
            //    }
            //};

            //context.Players.AddRange(players);

            //context.SaveChanges();

            //Console.WriteLine("Saved!");



            //var query = from p in context.Players
            //            select p;
            //Console.WriteLine("Query result: " + query.Count());
            //foreach (var player in query)
            //{
            //    Console.WriteLine(player.Name + " isBot: " + player.IsBot);
            //}
            //Console.WriteLine("Done!");

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
            //foreach (var player in players)
            //{
            //    testService.CreatePlayer(player);
            //}

            //Console.WriteLine("Saved!");


            //var result = testService.All();

            //Console.WriteLine("Query result: " + result.Count());
            //foreach (var player in result)
            //{
            //    Console.WriteLine(player.Name);
            //}
            //Console.WriteLine("Done!");
            //Console.ReadKey();

            /**/

            var gameLogic = new GameLogic();
            Console.WriteLine("Enter your name");
            var name = Console.ReadLine();
            Console.WriteLine("Enter number of players(from 2 up to 5)");

            var tmpString = Console.ReadLine();
            int numberOfPlayers = 2;
            if (Int32.TryParse(tmpString, out numberOfPlayers))
            {
                gameLogic.StartNewGame(name, numberOfPlayers);
            }
            else
            {
                Console.WriteLine("Does not compute!");
            }

        }


    }
}
