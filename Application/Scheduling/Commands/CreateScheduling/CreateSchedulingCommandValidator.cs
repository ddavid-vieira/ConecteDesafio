using FluentValidation;

namespace Application.Scheduling.Commands.CreateScheduling;

public class CreateSchedulingCommandValidator : AbstractValidator<CreateSchedulingCommand>
{
    public CreateSchedulingCommandValidator()
    {
        RuleFor(x => x.PatientId)
            .NotNull()
            .WithMessage("PatientId é obrigatório");

        RuleFor(x => x.AvailableTimeId)
            .NotNull()
            .WithMessage("AvailableTimeId é obrigatório");
    }
}