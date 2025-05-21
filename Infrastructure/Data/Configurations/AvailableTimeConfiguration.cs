using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class AvailableTimeConfiguration : IEntityTypeConfiguration<AvailableTime>
{
    public void Configure(EntityTypeBuilder<AvailableTime> builder)
    {
        builder.HasIndex(h => new { h.DoctorId, h.Hour })
            .IsUnique();
        builder.Property(x => x.Hour)
            .HasColumnType("timestamp without time zone");
    }
}