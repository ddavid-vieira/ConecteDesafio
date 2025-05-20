using System.Reflection;
using Application.Common.Interfaces;
using Domain.Entities;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Infrastructure.Data;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
    : IdentityDbContext<ApplicationUser>(options), IApplicationDbContext
{
    public DbSet<Doctor> Doctors => Set<Doctor>();

    public DbSet<Patient> Patients => Set<Patient>();
    public DbSet<AvailableTime> AvailableTimes => Set<AvailableTime>();
    public DbSet<Scheduling> Scheduling => Set<Scheduling>();

    DatabaseFacade IApplicationDbContext.Database => Database;

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}