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
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Round>()
                .HasRequired<Game>(r => r.Game)
                .WithMany(g => g.Rounds)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<Player>()
                .HasRequired<Game>(p => p.Game)
                .WithMany(g => g.Players)
                .WillCascadeOnDelete(true);

            //modelBuilder.Entity<Round>()
            //    .HasOptional<Player>(r => r.Winner);

            modelBuilder.Entity<RoundPlayer>()
                .HasRequired<Round>(rp => rp.Round)
                .WithMany(r => r.RoundPlayers)
                .WillCascadeOnDelete(true);

            modelBuilder.Entity<RoundPlayer>()
                .HasRequired<Player>(rp => rp.Player)
                .WithMany(p => p.PlayerRounds)
                .WillCascadeOnDelete(false);
        }

        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Round> Rounds { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<RoundPlayer> RoundPlayers { get; set; }
    }

}