using Application.Common.Exceptions;
using Application.Common.Interfaces;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Scheduling.Commands.CreateScheduling;

public class CreateSchedulingCommandHandler(IApplicationDbContext context)
    : IRequestHandler<CreateSchedulingCommand, int>
{
    public async Task<int> Handle(CreateSchedulingCommand request, CancellationToken cancellationToken)
    {
        await using var transaction = await context.Database.BeginTransactionAsync(cancellationToken);
        var patientExists = await context.Patients.AnyAsync(x => x.Id == request.PatientId, cancellationToken);

        if (!patientExists)
            throw new ValidationException([new ValidationFailure("PatientId", "Paciente não encontrado")]);

        var availableTime = await context.AvailableTimes
            .Include(x => x.Schedule)
            .FirstOrDefaultAsync(x => x.Id == request.AvailableTimeId, cancellationToken);

        if (availableTime == null)
            throw new ValidationException([new ValidationFailure("AvailableTimeId", "Horário não encontrado")]);

        if (availableTime.Schedule != null)
            throw new ValidationException([new ValidationFailure("AvailableTimeId", "Este horário já está agendado")]);

        var patientConflict = await context.Scheduling.AnyAsync(
            x => x.Id == request.PatientId && x.AvailableTime.Hour == availableTime.Hour, cancellationToken);

        if (patientConflict)
            throw new ValidationException([
                new ValidationFailure("AvailableTimeId", "O paciente já tem um agendamento nesse horário")
            ]);

        var entity = new Domain.Entities.Scheduling
        {
            PatientId = request.PatientId,
            AvailableTimeId = availableTime.Id,
        };

        context.Scheduling.Add(entity);

        await context.SaveChangesAsync(cancellationToken);

        await transaction.CommitAsync(cancellationToken);
        return entity.Id;
    }
}