using Application.Common.Interfaces;
using FluentValidation.Results;
using MediatR;
using Microsoft.EntityFrameworkCore;
using ValidationException = Application.Common.Exceptions.ValidationException;

namespace Application.AvailableTime.Commands.CreateAvailableTime;

public class CreateAvailableTimeCommandHandler(IApplicationDbContext context)
    : IRequestHandler<CreateAvailableTimeCommand, int>
{
    public async Task<int> Handle(CreateAvailableTimeCommand request, CancellationToken cancellationToken)
    {
        var exists =
            await context.AvailableTimes.AnyAsync(x => x.DoctorId == request.DoctorId && x.Hour == request.Hour,
                cancellationToken: cancellationToken);

        if (exists)
            throw new ValidationException([new ValidationFailure("Hour", "Horário já cadastrado")]);

        var entity = new Domain.Entities.AvailableTime
        {
            DoctorId = request.DoctorId,
            Hour = request.Hour,
        };
        context.AvailableTimes.Add(entity);

        await context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}