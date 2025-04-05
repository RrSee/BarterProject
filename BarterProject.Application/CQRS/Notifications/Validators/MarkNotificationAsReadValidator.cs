using BarterProject.Application.CQRS.Notifications.Commands.Requests;
using FluentValidation;

namespace BarterProject.Application.CQRS.Notifications.Validators;

public class MarkNotificationAsReadValidator : AbstractValidator<MarkNotificationAsReadCommand>
{
    public MarkNotificationAsReadValidator()
    {
        RuleFor(x => x.NotificationId)
            .NotEmpty().WithMessage("Notification ID is required.")
            .GreaterThan(0).WithMessage("Notification ID must be greater than zero.");
    }
}
