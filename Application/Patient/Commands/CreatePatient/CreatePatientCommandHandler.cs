using Application.Common.Interfaces;
using MediatR;

namespace Application.Patient.Commands.CreatePatient;

public class CreatePatientCommandHandler(IApplicationDbContext context, IIdentityService identityService)
    : IRequestHandler<CreatePatientCommand, int>
{
    public async Task<int> Handle(CreatePatientCommand request, CancellationToken cancellationToken)
    {
        var result =
            await identityService.CreatePatientUserAsync(request.Email, request.Password);

        var entity = new Domain.Entities.Patient()
            { Name = request.Name, Weight = request.Weight, ApplicationUserId = result.UserId };

        context.Patients.Add(entity);

        await context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}