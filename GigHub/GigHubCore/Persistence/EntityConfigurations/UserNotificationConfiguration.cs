using GigHubCore.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GigHubCore.Persistence.EntityConfigurations
{
    public class UserNotificationConfiguration : IEntityTypeConfiguration<UserNotification>
    {
        public void Configure(EntityTypeBuilder<UserNotification> builder)
        {
            builder
                .HasKey(k => new { k.UserId, k.NotificationId });

            builder
                .HasOne(u => u.User)
                .WithMany(u => u.UserNotifications)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
