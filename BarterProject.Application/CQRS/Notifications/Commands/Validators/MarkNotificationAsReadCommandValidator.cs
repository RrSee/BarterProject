using BarterProject.Application.CQRS.Notifications.Commands.Requests;
using FluentValidation;

namespace BarterProject.Application.CQRS.Notifications.Commands.Validators;

public class MarkNotificationAsReadCommandValidator : AbstractValidator<MarkNotificationAsReadCommand>
{
    public MarkNotificationAsReadCommandValidator()
    {
        RuleFor(x => x.NotificationId)
            .GreaterThan(0).WithMessage("Notification ID must be greater than zero.");
    }
}
