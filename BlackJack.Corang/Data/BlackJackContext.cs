using System;
using BlackJack.Corang.Models;
using Microsoft.EntityFrameworkCore;

namespace BlackJack.Corang.Data
{
    public class BlackJackContext : DbContext
    {
        public BlackJackContext(DbContextOptions<BlackJackContext> options) : base(options)
        {


        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Round>()
                .HasOne<Game>(r => r.Game)
                .WithMany(g => g.Rounds)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Player>()
                .HasOne<Game>(p => p.Game)
                .WithMany(g => g.Players)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RoundPlayer>()
                .HasOne<Round>(rp => rp.Round)
                .WithMany(r => r.RoundPlayers)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<RoundPlayer>()
                .HasOne<Player>(rp => rp.Player)
                .WithMany(p => p.PlayerRounds)
                .OnDelete(DeleteBehavior.Restrict);
        }

        public virtual DbSet<Game> Games { get; set; }
        public virtual DbSet<Round> Rounds { get; set; }
        public virtual DbSet<Player> Players { get; set; }
        public virtual DbSet<RoundPlayer> RoundPlayers { get; set; }
    }

}