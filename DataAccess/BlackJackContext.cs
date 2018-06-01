namespace DataAccess
{
    using DataAccess.Models;
    using System;
    using System.Data.Entity;
    using System.Linq;

    public class BlackJackContext : DbContext
    {
        public BlackJackContext()
            : base("name=BlackJackContext")
        {

            
        }

        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Round> Rounds { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<Round_Player> Round_Player { get; set; }
    }

}