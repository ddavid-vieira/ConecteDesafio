using MediatR;

namespace Application.Scheduling.Commands.DeleteScheduling;

public record DeleteSchedulingCommand(int Id) : IRequest;