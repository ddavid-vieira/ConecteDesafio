using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Ardalis.GuardClauses;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.AvailableTime.Commands.DeleteAvailableTime;

public class DeleteAvailableTimeCommandHandler(IApplicationDbContext context)
    : IRequestHandler<DeleteAvailableTimeCommand>
{
    public async Task Handle(DeleteAvailableTimeCommand request, CancellationToken cancellationToken)
    {
        var doctor = await context.Doctors
            .AsNoTracking()
            .FirstOrDefaultAsync(x => x.ApplicationUserId == request.UserId, cancellationToken);

        if (doctor == null)
            Guard.Against.NotFound(request.UserId, doctor);

        var entity = context.AvailableTimes
            .Include(x => x.Schedule)
            .FirstOrDefault(x => x.Id == request.Id && x.DoctorId == doctor.Id);

        Guard.Against.NotFound(request.Id, entity);

        if (entity.Schedule != null)
            throw new ValidationException([
                new ValidationFailure("Id", "Não é possível excluir um horário com um agendamento")
            ]);
        context.AvailableTimes.Remove(entity);

        await context.SaveChangesAsync(cancellationToken);
    }
}