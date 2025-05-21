using Application.Common.Models;
using Application.Common.Models.Scheduling;
using Application.Scheduling.Queries.GetSchedulingsWithPagination;
using MediatR;

namespace Application.Scheduling.Queries.GetPatientSchedulingsWithPagination;

public record GetPatientSchedulingsWithPaginationQuery(string UserId, GetSchedulingPaginationParams PaginationParams)
    : IRequest<PaginatedList<PatientSchedulingsGroupedByDateDto>>;