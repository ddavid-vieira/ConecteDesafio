using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class SchedulingConfiguration : IEntityTypeConfiguration<Scheduling>
{
    public void Configure(EntityTypeBuilder<Scheduling> builder)
    {
        builder
            .HasIndex(h => new { h.PatientId, h.AvailableTimeId })
            .IsUnique();
    }
}