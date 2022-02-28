using MeetupPlatform.Common.Models.MeetUps;
using MeetupPlatform.Common.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace MeetupPlatform.Infrastructure.Database
{
    public class MeetupPlatformContext : DbContext
    {
        public DbSet<User> Users { get; set; }

        public DbSet<Follower> Followers { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<Meetup> Meetups { get; set; }

        public DbSet<Place> Places { get; set; }

        public DbSet<Step> Steps { get; set; }

        public MeetupPlatformContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=MeetupDb;Username=postgres;Password=DbVKR4ca");
        }
    }
}
