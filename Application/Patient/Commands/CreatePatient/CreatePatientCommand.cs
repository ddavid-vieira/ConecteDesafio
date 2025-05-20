using MediatR;

namespace Application.Patient.Commands.CreatePatient;

public record CreatePatientCommand : IRequest<int>
{
    public string UserName { get; init; } = string.Empty;

    public string Email { get; init; } = string.Empty;

    public string Password { get; init; } = string.Empty;

    public string Name { get; init; } = string.Empty;

    public double Weight { get; init; }
}