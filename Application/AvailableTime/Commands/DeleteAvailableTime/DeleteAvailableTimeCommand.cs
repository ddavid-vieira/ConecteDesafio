using Application.Common.Security;
using Domain.Constants;
using MediatR;

namespace Application.AvailableTime.Commands.DeleteAvailableTime;

[Authorize(Roles = Roles.Doctor)]
public record DeleteAvailableTimeCommand(int Id, string UserId) : IRequest;