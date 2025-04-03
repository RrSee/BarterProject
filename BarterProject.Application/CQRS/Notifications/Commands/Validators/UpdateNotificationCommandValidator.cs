using BarterProject.Application.CQRS.Notifications.Commands.Requests;
using FluentValidation;

namespace BarterProject.Application.CQRS.Notifications.Commands.Validators;

public class UpdateNotificationCommandValidator : AbstractValidator<UpdateNotificationCommand>
{
    public UpdateNotificationCommandValidator()
    {
        RuleFor(x => x.NotificationId)
            .GreaterThan(0).WithMessage("Notification ID must be greater than zero.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description must be at most 500 characters.");

        RuleFor(x => x.UpdatedBy)
            .GreaterThan(0).WithMessage("UpdatedBy must be greater than zero.");
    }
}
