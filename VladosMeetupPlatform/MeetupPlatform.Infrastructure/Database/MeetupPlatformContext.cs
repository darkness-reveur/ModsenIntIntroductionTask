using MeetupPlatform.Common.Models.AuthenticationModels;
using MeetupPlatform.Common.Models.MeetUps;
using MeetupPlatform.Common.Models.Users;
using Microsoft.EntityFrameworkCore;

namespace MeetupPlatform.Infrastructure.Database
{
    public class MeetupPlatformContext : DbContext
    {
        public MeetupPlatformContext(DbContextOptions<MeetupPlatformContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<Meetup> Meetups { get; set; }

        public DbSet<Place> Places { get; set; }

        public DbSet<Step> Steps { get; set; }

        public DbSet<AccessDataEntity> AccessData { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
