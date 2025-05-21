using FluentValidation;

namespace Application.Scheduling.Queries.GetPatientSchedulingsWithPagination;

public class GetPatientSchedulingsWithPaginationValidator : AbstractValidator<GetPatientSchedulingsWithPaginationQuery>
{
    public GetPatientSchedulingsWithPaginationValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty()
            .WithMessage("UserId não pode ser nulo.");

        RuleFor(x => x.PaginationParams.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber precisa ser maior ou igual a 1.");

        RuleFor(x => x.PaginationParams.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize precisa ser maior ou igual a 1.");
    }
}