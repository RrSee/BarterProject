using BarterProject.Application.CQRS.Notifications.Queries.Requests;
using FluentValidation;

namespace BarterProject.Application.CQRS.Notifications.Validators;

public sealed class GetByIdNotificationValidator : AbstractValidator<GetByIdNotificationQuery>
{
    public GetByIdNotificationValidator()
    {
        RuleFor(x => x.NotificationId)
            .NotEmpty().WithMessage("Notification ID cannot be empty.")
            .GreaterThan(0).WithMessage("Notification ID must be greater than 0.");
    }
}
