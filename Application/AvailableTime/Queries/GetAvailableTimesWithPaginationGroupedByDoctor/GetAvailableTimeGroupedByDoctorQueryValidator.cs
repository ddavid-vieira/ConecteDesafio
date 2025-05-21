using FluentValidation;

namespace Application.AvailableTime.Queries.GetAvailableTimesWithPaginationGroupedByDoctor;

public class GetAvailableTimeGroupedByDoctorQueryValidator : AbstractValidator<GetAvailableTimeGroupedByDoctorQuery>
{
    public GetAvailableTimeGroupedByDoctorQueryValidator()
    {
        RuleFor(x => x.PageNumber)
            .GreaterThanOrEqualTo(1).WithMessage("PageNumber precisa ser maior ou igual a 1.");

        RuleFor(x => x.PageSize)
            .GreaterThanOrEqualTo(1).WithMessage("PageSize precisa ser maior ou igual a 1.");
    }
}