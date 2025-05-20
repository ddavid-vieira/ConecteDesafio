using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configurations;

public class DoctorConfiguration : IEntityTypeConfiguration<Doctor>
{
    public void Configure(EntityTypeBuilder<Doctor> builder)
    {
        builder.HasOne<ApplicationUser>()
            .WithOne(x => x.Doctor)
            .HasForeignKey<Doctor>(x => x.ApplicationUserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Restrict);
    }
}