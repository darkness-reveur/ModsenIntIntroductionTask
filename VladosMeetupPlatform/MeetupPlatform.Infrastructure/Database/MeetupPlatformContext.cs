using MeetupPlatform.Common.Models.HelperModels;
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

        public DbSet<Follower> Followers { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Permission> Permissions { get; set; }

        public DbSet<Meetup> Meetups { get; set; }

        public DbSet<Place> Places { get; set; }

        public DbSet<Step> Steps { get; set; }

        public DbSet<Organizer> Organizers { get; set; }

        public DbSet<MeetupVisitor> Visitors { get; set; }

        public DbSet<RolePermission> RolePermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //modelBuilder.Entity<Meetup>().HasData(
            //    new Meetup[]
            //    {
            //    new Meetup {
            //        Id = 1,
            //        Name = "ITechMeetup",
            //        Description = "",
            //        Place = new Place
            //        {
            //                Id = 1,
            //                Name = "Тайм кофе небо",
            //                LinkToGoogleMap = "https://www.google.com/maps/place/%D0%A2%D0%B0%D0%B9%D0%BC-%D0%BA%D0%B0%D1%84%D0%B5+%22%D0%9D%D0%B5%D0%B1%D0%BE%22/@53.8952025,30.3302433,17z/data=!3m1!4b1!4m5!3m4!1s0x46d051ee8c6a575d:0x2ed2847bd7ca779a!8m2!3d53.8952529!4d30.33255?hl=ru",
            //                CountOfPlaces = 40,
            //                Description = "",
            //        },
            //        StartTime = DateTime.Now,
            //        EndTime = DateTime.Now,
            //        CountOfVisitors = 0,
            //        UserOrganizer = new User
            //        {
            //            Id = 1,
            //            Age = 20,
            //            Name = "Org",
            //            Email = "uvapuvp@mail.ru",
            //            IsCompany = true,
            //            IsUserBlocked = false,
            //            Role = new Role
            //            {
            //                Id=1,
            //                Name = "admin",
            //                Permissions = new List<Permission>
            //                {
            //                    new Permission {Id = 1, Name = "AllPermisions"}
            //                }
            //            }
            //        },
            //        IsMeetForAuthorizedUsers = false,
            //    }
            //    });
        }
    }
}
