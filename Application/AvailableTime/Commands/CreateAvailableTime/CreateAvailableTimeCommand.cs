using Application.Common.Security;
using Domain.Constants;
using MediatR;

namespace Application.AvailableTime.Commands.CreateAvailableTime;

[Authorize(Roles = Roles.Doctor)]
public class CreateAvailableTimeCommand : IRequest<int>
{
    public int DoctorId { get; set; }

    public DateTime Hour { get; set; }
}