﻿using GigHubCore.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace GigHubCore.Persistence.EntityConfigurations
{
    public class GigConfiguration : IEntityTypeConfiguration<Gig>
    {
        public void Configure(EntityTypeBuilder<Gig> builder)
        {
            builder
                .Property(g => g.ArtistId)
                .IsRequired();

            builder
                .Property(g => g.GenreId)
                .IsRequired();

            builder
                .Property(g => g.Venue)
                .IsRequired()
                .HasMaxLength(255);

            builder
                .HasMany(g => g.Attendances)
                .WithOne(g => g.Gig)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
