﻿using MeetupPlatform.Common.Models.HelperModels;
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

        public DbSet<MeetupVisitor> Visitors { get; set; }

        public DbSet<RolePermission> RolePermissions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            
        }
    }
}
