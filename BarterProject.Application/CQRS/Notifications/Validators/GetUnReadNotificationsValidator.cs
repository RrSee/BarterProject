using BarterProject.Application.CQRS.Notifications.Queries.Requests;
using FluentValidation;

namespace BarterProject.Application.CQRS.Notifications.Validators;

public sealed class GetUnReadNotificationsValidator : AbstractValidator<GetUnreadNotificationsQuery>
{
    public GetUnReadNotificationsValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID cannot be empty.")
            .GreaterThan(0).WithMessage("User ID must be greater than 0.");
    }
}
