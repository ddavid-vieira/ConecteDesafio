using Application.Common.Models;
using Application.Common.Models.AvailableTime;
using MediatR;

namespace Application.AvailableTime.Queries.GetAvailableTimesWithPaginationGroupedByDoctor;

public record GetAvailableTimeGroupedByDoctorQuery : IRequest<PaginatedList<AvailableTimeGroupedByDoctorDto>>
{
    public int PageNumber { get; init; } = 1;
    public int PageSize { get; init; } = 10;
}