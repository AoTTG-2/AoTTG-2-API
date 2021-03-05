using AoTTG2.IDS.Data.Dao;
using AoTTG2.IDS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using AoTTG2.IDS.Data.Dao.Assets;

namespace AoTTG2.IDS.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ReportDao> Reports { get; set; }
        public DbSet<BadgeDao> Badges { get; set; }

        public DbSet<SkinDao> Skins { get; set; }
        public DbSet<SetHumanDao> SetHumans { get; set; }
        public DbSet<SetTitanDao> SetTitans { get; set; }

        // Composite Tables
        public DbSet<UserBadgeDao> UserBadges { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<UserBadgeDao>().HasKey(x => new { x.UserId, x.BadgeId });

            builder.Entity<UserBadgeDao>()
                .HasOne(x => x.User)
                .WithMany(x => x.Badges)
                .HasForeignKey(x => x.UserId);
        }
    }
}
