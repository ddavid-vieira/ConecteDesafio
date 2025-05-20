using FluentValidation;

namespace Application.AvailableTime.Commands.CreateAvailableTime;

public class CreateAvailableTimeCommandValidator
    : AbstractValidator<CreateAvailableTimeCommand>
{
    public CreateAvailableTimeCommandValidator(TimeProvider timeProvider)
    {
        RuleFor(x => x.DoctorId)
            .NotNull()
            .WithMessage("DoctorId é obrigatório");

        RuleFor(command => command.Hour)
            .NotNull()
            .WithMessage("Horário é obrigatório")
            .GreaterThan(timeProvider.GetLocalNow().DateTime)
            .WithMessage("O horário precisa ser maior que o horário atual");
    }
}