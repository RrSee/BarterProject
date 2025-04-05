using BarterProject.Application.CQRS.Users.Commads.Requests;
using FluentValidation;

namespace BarterProject.Application.CQRS.Users.Validators;

public sealed class RefreshTokenValidator : AbstractValidator<RefreshTokenRequest>
{
    public RefreshTokenValidator()
    {
        RuleFor(x => x.Token)
            .NotEmpty().WithMessage("Token cannot be empty.")
            .MinimumLength(10).WithMessage("Token must be at least 10 characters long.")
            .MaximumLength(700).WithMessage("Token must be at most 700 characters long.");
    }
}