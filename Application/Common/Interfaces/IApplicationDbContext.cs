using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Domain.Entities.Doctor> Doctors { get; }

    DbSet<Domain.Entities.Patient> Patients { get; }

    DbSet<Domain.Entities.AvailableTime> AvailableTimes { get; }

    DbSet<Domain.Entities.Scheduling> Scheduling { get; }

    DatabaseFacade Database { get; }
    
    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}