using BarterProject.Application.CQRS.Users.Queries.Requests;
using FluentValidation;

namespace BarterProject.Application.CQRS.Users.Validators;

public sealed class GetByEmailUserValidator : AbstractValidator<GetByEmailUserRequest>
{
    public GetByEmailUserValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email cannot be empty.")
            .EmailAddress().WithMessage("Invalid email format.");
    }
}
