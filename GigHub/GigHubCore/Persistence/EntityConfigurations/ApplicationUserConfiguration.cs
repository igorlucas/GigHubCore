using GigHubCore.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GigHubCore.Persistence.EntityConfigurations
{
    public class ApplicationUserConfiguration : IEntityTypeConfiguration<ApplicationUser>
    {
        public void Configure(EntityTypeBuilder<ApplicationUser> builder)
        {
            builder
                .Property(u => u.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder
                .HasMany(u => u.Followers)
                .WithOne(f => f.Followee)
                .OnDelete(DeleteBehavior.Restrict);

            builder
                .HasMany(u => u.Followees)
                .WithOne(f => f.Follower)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
