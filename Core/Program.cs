
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
            //        Name = "another user"
            //    },
            //    new Player()
            //    {
            //        Id = Guid.NewGuid(),
            //        Name = "user"
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
            //    Console.WriteLine(player.Name);
            //}
            //Console.WriteLine("Done!");

            /**/

            Console.WriteLine("Begin!");

            var testService = new PlayerService();

            var players = new Player[]
            {
                new Player() 
                {
                    Id = Guid.NewGuid(),
                    Name = "SomeUser"
                }
            };
            foreach (var player in players)
            {
                testService.CreatePlayer(player);
            }

            Console.WriteLine("Saved!");


            var result = testService.All();

            Console.WriteLine("Query result: " + result.Count());
            foreach (var player in result)
            {
                Console.WriteLine(player.Name);
            }
            Console.WriteLine("Done!");
            Console.ReadKey();
        }


    }
}
