using System.Collections.Generic;
using comp231_002__Team1_TeamUp_SportsBooking.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace comp231_002__Team1_TeamUp_SportsBooking.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Player> Players { get; set; } = default!;
        public DbSet<Team> Teams { get; set; } = default!;
        public DbSet<Game> Games { get; set; } = default!;

        public DbSet<Court> Courts { get; set; } = default!;
        public DbSet<Booking> Bookings { get; set; } = default!;
        public DbSet<Notification> Notifications { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Player>().ToTable("Player");
            modelBuilder.Entity<Team>().ToTable("Team");
            modelBuilder.Entity<Court>().ToTable("Court");
            modelBuilder.Entity<Game>().ToTable("Game");
            modelBuilder.Entity<Booking>().ToTable("Booking");
            modelBuilder.Entity<Notification>().ToTable("Notification");

        }
    }
}
