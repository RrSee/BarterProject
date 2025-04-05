using BarterProject.Application.CQRS.Users.Queries.Requests;
using FluentValidation;

namespace BarterProject.Application.CQRS.Users.Validators;

public sealed class GetByIdUserValidator : AbstractValidator<GetByIdUserRequest>
{
    public GetByIdUserValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID cannot be empty.")
            .GreaterThan(0).WithMessage("User ID must be greater than 0.");
    }
}
