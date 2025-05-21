using Application.Common.Models;
using Application.Common.Models.Scheduling;
using Application.Common.Security;
using Domain.Constants;
using MediatR;

namespace Application.Scheduling.Queries.GetSchedulingsWithPagination;

[Authorize(Roles = Roles.Doctor)]
public record GetSchedulingsWithPaginationQuery(string UserId, GetSchedulingPaginationParams PaginationParams)
    : IRequest<PaginatedList<DoctorSchedulingsGroupedByDateDto>>;

public record GetSchedulingPaginationParams
{
    public int PageNumber { get; init; } = 1;

    public int PageSize { get; init; } = 10;
}