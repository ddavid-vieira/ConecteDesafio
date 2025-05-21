using Application.Common.Interfaces;
using Application.Common.Mappings;
using Application.Common.Models;
using Application.Common.Models.AvailableTime;
using MediatR;

namespace Application.AvailableTime.Queries.GetAvailableTimesWithPaginationGroupedByDoctor;

public class GetAvailableTimeGroupedByDoctorQueryHandler(IApplicationDbContext context)
    : IRequestHandler<GetAvailableTimeGroupedByDoctorQuery, PaginatedList<AvailableTimeGroupedByDoctorDto>>
{
    public async Task<PaginatedList<AvailableTimeGroupedByDoctorDto>> Handle(
        GetAvailableTimeGroupedByDoctorQuery request,
        CancellationToken cancellationToken)
    {
        return await context.AvailableTimes
            .Where(x => x.Schedule == null)
            .OrderBy(x => x.Doctor.Name)
            .GroupBy(x => x.Doctor)
            .Select(x => new AvailableTimeGroupedByDoctorDto
            {
                DoctorId = x.Key.Id,
                DoctorName = x.Key.Name,
                AvailableTimes = x.GroupBy(x => x.Hour.Date)
                    .Select(y => new AvailableDayDto
                    {
                        Date = DateOnly.FromDateTime(y.Key),
                        Times = y.Select(z => TimeOnly.FromDateTime(z.Hour))
                            .OrderBy(x => x)
                            .AsEnumerable()
                    })
                    .OrderByDescending(x => x.Date)
                    .ToList()
            })
            .OrderBy(x => x.DoctorName)
            .PaginatedListAsync(request.PageNumber, request.PageSize);
    }
}