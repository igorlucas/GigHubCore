using GigHubCore.Core.Models;
using GigHubCore.Persistence.EntityConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace GigHubCore.Persistence.DbContexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public DbSet<Gig> Gigs { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Attendance> Attendances { get; set; }
        public DbSet<Following> Followings { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new GigConfiguration());

            builder.ApplyConfiguration(new AttendanceConfiguration());

            builder.ApplyConfiguration(new FollowingConfiguration());

            builder.ApplyConfiguration(new ApplicationUserConfiguration());

            builder.ApplyConfiguration(new UserNotificationConfiguration());

            base.OnModelCreating(builder);
        }
    }
}
