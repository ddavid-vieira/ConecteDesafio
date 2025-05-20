using Application.Common.Security;
using Domain.Constants;
using MediatR;

namespace Application.Scheduling.Commands.CreateScheduling;

[Authorize(Roles = Roles.Patient)]
public record CreateSchedulingCommand : IRequest<int>
{
    public int PatientId { get; init; }

    public int AvailableTimeId { get; init; }
}