using BarterProject.Application.CQRS.BarterRequests.Commands.Requests;
using FluentValidation;

namespace BarterProject.Application.CQRS.BarterRequests.Validators;

public class CreateBarterRequestValidator : AbstractValidator<CreateBarterRequestRequest>
{
    public CreateBarterRequestValidator()
    {
        RuleFor(x => x.SenderUserId)
            .GreaterThan(0).WithMessage("Sender User ID must be greater than zero.")
            .NotEmpty().WithMessage("Sender user ID is required.");

        RuleFor(x => x.ReceiverUserId)
            .GreaterThan(0).WithMessage("Receiver User ID must be greater than zero.")
            .NotEmpty().WithMessage("Receiver user ID is required.");

        RuleFor(x => x.SenderItemId)
            .GreaterThan(0).WithMessage("Sender Item ID must be greater than zero.")
            .NotEmpty().WithMessage("Sender item ID is required.");

        RuleFor(x => x.ReceiverItemId)
            .GreaterThan(0).WithMessage("Receiver Item ID must be greater than zero.")
            .NotEmpty().WithMessage("Receiver item ID is required.");
        RuleFor(x => x.SenderUserId)
            .NotEqual(x => x.ReceiverUserId).WithMessage("Sender User ID and Receiver User ID cannot be the same.");
        RuleFor(x => x.SenderItemId)
            .NotEqual(x => x.ReceiverItemId).WithMessage("Sender Item ID and Receiver Item ID cannot be the same.");
    }
}
