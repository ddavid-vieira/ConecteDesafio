using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using Application.Common.Models.Scheduling;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Scheduling.Queries.GetSchedulingsWithPagination;

public class GetSchedulingsWithPaginationQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetSchedulingsWithPaginationQuery,
        PaginatedList<DoctorSchedulingsGroupedByDateDto>>
{
    public async Task<PaginatedList<DoctorSchedulingsGroupedByDateDto>> Handle(
        GetSchedulingsWithPaginationQuery request,
        CancellationToken cancellationToken)
    {
        return await context.Scheduling.Include(x => x.Patient)
            .Include(x => x.AvailableTime)
            .Where(x => x.AvailableTime.Doctor.ApplicationUserId == request.UserId)
            .GroupBy(x => x.AvailableTime.Hour.Date)
            .Select(x => new DoctorSchedulingsGroupedByDateDto
            {
                Date = DateOnly.FromDateTime(x.Key),
                Schedulings = x.Select(y => new DoctorSchedulingDto
                    {
                        PatientName = y.Patient.Name,
                        Weight = y.Patient.Weight,
                        Hour = TimeOnly.FromDateTime(y.AvailableTime.Hour)
                    }).OrderBy(x => x.Hour)
                    .AsEnumerable()
            })
            .OrderByDescending(x => x.Date)
            .PaginatedListAsync(request.PaginationParams.PageNumber, request.PaginationParams.PageSize);
    }
}