using BarterProject.Application.CQRS.BarterRequests.Commands.Requests;
using FluentValidation;

namespace BarterProject.Application.CQRS.BarterRequests.Validators;

public class UpdateBarterRequestStatusValidator : AbstractValidator<UpdateBarterRequestStatusRequest>
{
    public UpdateBarterRequestStatusValidator()
    {
        RuleFor(x => x.RequestId)
            .NotEmpty().WithMessage("Barter request ID is required.")
            .GreaterThan(0).WithMessage("Barter ID must be greater than zero.");

        RuleFor(x => x.Status)
            .NotEmpty().WithMessage("Status is required.")
            .IsInEnum().WithMessage("Invalid status value.");
    }
}
