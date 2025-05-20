using MediatR;

namespace Application.Doctor.Commands.CreateDoctor;

public record CreateDoctorCommand : IRequest<int>

{
    public string UserName { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;

    public string? Name { get; init; }

    public string? Crm { get; init; }
}