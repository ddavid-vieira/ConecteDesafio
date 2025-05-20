using FluentValidation;

namespace Application.Doctor.Commands.CreateDoctor;

public class CreateDoctorCommandValidator : AbstractValidator<CreateDoctorCommand>
{
    public CreateDoctorCommandValidator()
    {
        RuleFor(v => v.UserName)
            .NotEmpty()
            .WithMessage("O nome de usuário é obrigatório");

        RuleFor(v => v.Email)
            .NotEmpty()
            .WithMessage("O e-mail é obrigatório")
            .EmailAddress()
            .WithMessage("O e-mail deve ser um endereço de e-mail válido")
            .MaximumLength(200)
            .WithMessage("O e-mail deve ter no máximo 200 caracteres");

        RuleFor(v => v.Password)
            .NotEmpty()
            .WithMessage("A senha é obrigatória")
            .MinimumLength(6)
            .WithMessage("A senha deve ter no mínimo 6 caracteres")
            .MaximumLength(20)
            .WithMessage("A senha deve ter no máximo 20 caracteres")
            .Matches("[0-9]")
            .WithMessage("A senha deve conter pelo menos um número")
            .Matches("[a-z]")
            .WithMessage("A senha deve conter pelo menos uma letra minúscula")
            .Matches("[A-Z]")
            .WithMessage("A senha deve conter pelo menos uma letra maiúscula")
            .Matches("[^a-zA-Z0-9]")
            .WithMessage("A senha deve conter pelo menos um caractere especial");

        RuleFor(v => v.Name)
            .NotEmpty()
            .WithMessage("O nome é obrigatório")
            .MaximumLength(200)
            .WithMessage("O nome deve ter no máximo 200 caracteres");

        RuleFor(v => v.Crm)
            .NotEmpty()
            .WithMessage("O CRM é obrigatório")
            .MaximumLength(20)
            .WithMessage("O CRM deve ter no máximo 20 caracteres");
    }
}