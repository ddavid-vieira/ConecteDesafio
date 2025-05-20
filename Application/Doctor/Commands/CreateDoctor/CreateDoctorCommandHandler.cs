using Application.Common.Interfaces;
using Domain.Enums;
using MediatR;

namespace Application.Doctor.Commands.CreateDoctor;

public class CreateDoctorCommandHandler(IApplicationDbContext context, IIdentityService identityService)
    : IRequestHandler<CreateDoctorCommand, int>
{
    public async Task<int> Handle(CreateDoctorCommand request, CancellationToken cancellationToken)
    {
        var result =
            await identityService.CreateDoctorUserAsync(request.Email, request.Password);

        var entity = new Domain.Entities.Doctor
            { Name = request.Name, Crm = request.Crm, ApplicationUserId = result.UserId };

        context.Doctors.Add(entity);

        await context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}