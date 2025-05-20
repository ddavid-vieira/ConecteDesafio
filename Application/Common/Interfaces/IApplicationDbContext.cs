using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.Common.Interfaces;

public interface IApplicationDbContext
{
    DbSet<Domain.Entities.Doctor> Doctors { get; }

    DbSet<Domain.Entities.Patient> Patients { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken);
}