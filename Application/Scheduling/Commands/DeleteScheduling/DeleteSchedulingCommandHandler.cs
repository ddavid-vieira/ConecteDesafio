using Application.Common.Interfaces;
using Ardalis.GuardClauses;
using MediatR;

namespace Application.Scheduling.Commands.DeleteScheduling;

public class DeleteSchedulingCommandHandler(IApplicationDbContext context) : IRequestHandler<DeleteSchedulingCommand>
{
    public async Task Handle(DeleteSchedulingCommand request, CancellationToken cancellationToken)
    {
        var entity = await context.Scheduling.FindAsync(request.Id, cancellationToken);

        Guard.Against.NotFound(request.Id, entity);

        context.Scheduling.Remove(entity);

        await context.SaveChangesAsync(cancellationToken);
    }
}