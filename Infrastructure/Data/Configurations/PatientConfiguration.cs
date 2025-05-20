using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class PatientConfiguration : IEntityTypeConfiguration<Patient>
{
    public void Configure(EntityTypeBuilder<Patient> builder)
    {
        builder.HasOne<ApplicationUser>()
            .WithOne(x => x.Patient)
            .HasForeignKey<Patient>(x => x.ApplicationUserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}