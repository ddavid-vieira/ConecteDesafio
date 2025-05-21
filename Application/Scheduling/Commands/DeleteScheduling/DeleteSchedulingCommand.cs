using Application.Common.Security;
using Domain.Constants;
using MediatR;

namespace Application.Scheduling.Commands.DeleteScheduling;

[Authorize(Roles = Roles.Patient)]
public record DeleteSchedulingCommand(int Id) : IRequest;