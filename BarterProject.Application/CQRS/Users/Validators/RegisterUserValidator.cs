using BarterProject.Application.CQRS.Users.Commads.Requests;
using FluentValidation;

namespace BarterProject.Application.CQRS.Users.Validators;

public sealed class RegisterUserValidator : AbstractValidator<RegisterUserRequest>
{
    public RegisterUserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("First name cannot be empty.")
            .MinimumLength(2).WithMessage("First name must be at least 2 characters long.")
            .MaximumLength(30).WithMessage("Maximum length is 30");

        RuleFor(x => x.Surname)
            .NotEmpty().WithMessage("Last name cannot be empty.")
            .MinimumLength(2).WithMessage("Last name must be at least 2 characters long.")
            .MaximumLength(30).WithMessage("Maximum length is 30");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email cannot be empty.")
            .EmailAddress().WithMessage("Invalid email format.")
            .MaximumLength(70).WithMessage("Maximum length is 70");

        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Password cannot be empty.")
            .MinimumLength(6).WithMessage("Password must be at least 6 characters long.")
            .MaximumLength(20).WithMessage("Maximum length is 20");

        RuleFor(x => x.Telephone)
            .NotEmpty().WithMessage("Telephone cannot be empty.")
            .MaximumLength(50).WithMessage("Maximum length is 50")
            .Matches(@"^\+994(5[015]|7[07])\d{7}$").WithMessage("Mobile phone format is +994");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address cannot be empty.")
            .MaximumLength(100).WithMessage("Maximum length is 100");
    }
}
