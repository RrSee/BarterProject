using BarterProject.Application.CQRS.Notifications.Commands.Requests;
using FluentValidation;

namespace BarterProject.Application.CQRS.Notifications.Commands.Validators;

public class CreateNotificationCommandValidator : AbstractValidator<CreateNotificationCommand>
{
    public CreateNotificationCommandValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description must be at most 500 characters.");

        RuleFor(x => x.SendedUserId)
            .GreaterThan(0).WithMessage("SendedUserId must be greater than zero.");

        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("UserId must be greater than zero.");

        RuleFor(x => x.SendedUserId)
            .NotEqual(x => x.UserId).WithMessage("SendedUserId and UserId cannot be the same.");
    }
}
