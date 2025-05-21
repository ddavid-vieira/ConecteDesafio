using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using Application.Common.Models.Scheduling;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Scheduling.Queries.GetPatientSchedulingsWithPagination;

public class GetPatientSchedulingsWithPaginationQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetPatientSchedulingsWithPaginationQuery,
        PaginatedList<PatientSchedulingsGroupedByDateDto>>
{
    public async Task<PaginatedList<PatientSchedulingsGroupedByDateDto>> Handle(
        GetPatientSchedulingsWithPaginationQuery request, CancellationToken cancellationToken)
    {
        return await context.Scheduling
            .Include(x => x.AvailableTime)
            .ThenInclude(x => x.Doctor)
            .Where(x => x.Patient.ApplicationUserId == request.UserId)
            .GroupBy(x => x.AvailableTime.Hour.Date)
            .Select(x => new PatientSchedulingsGroupedByDateDto
            {
                Date = DateOnly.FromDateTime(x.Key),
                Schedulings = x.Select(y => new PatientSchedulingDto()
                    {
                        DoctorName = y.AvailableTime.Doctor.Name,
                        Hour = TimeOnly.FromDateTime(y.AvailableTime.Hour)
                    }).OrderBy(x => x.Hour)
                    .AsEnumerable()
            })
            .OrderByDescending(x => x.Date)
            .PaginatedListAsync(request.PaginationParams.PageNumber, request.PaginationParams.PageSize);
    }
}