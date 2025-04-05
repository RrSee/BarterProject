using BarterProject.Application.CQRS.Users.Commads.Requests;
using FluentValidation;

namespace BarterProject.Application.CQRS.Users.Validators;

public sealed class UpdateUserValidator : AbstractValidator<UpdateUserRequest>
{
    public UpdateUserValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("First name cannot be empty.")
            .MinimumLength(2).WithMessage("First name must be at least 2 characters long.");

        RuleFor(x => x.Surname)
            .NotEmpty().WithMessage("Last name cannot be empty.")
            .MinimumLength(2).WithMessage("Last name must be at least 2 characters long.");

        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email cannot be empty.")
            .EmailAddress().WithMessage("Invalid email format.");

        RuleFor(x => x.Telephone)
            .NotEmpty().WithMessage("Telephone cannot be empty.")
            .Matches(@"^\+?[0-9]{10,15}$").WithMessage("Invalid telephone format. It should be a valid phone number.")
            .MaximumLength(50).WithMessage("Maximum length is 50")
            .Matches(@"^\+994(5[015]|7[07])\d{7}$").WithMessage("Mobile phone format is +994");

        RuleFor(x => x.Address)
            .NotEmpty().WithMessage("Address cannot be empty.")
            .MaximumLength(200).WithMessage("Maximum length is 200");
    }
}