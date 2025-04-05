using BarterProject.Application.CQRS.BarterRequests.Commands.Requests;
using FluentValidation;

namespace BarterProject.Application.CQRS.BarterRequests.Validators;

public sealed class DeleteBarterRequestValidator : AbstractValidator<DeleteBarterRequestRequest>
{
    public DeleteBarterRequestValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Barter request ID is required.")
            .GreaterThan(0).WithMessage("Barter ID must be greater than zero.");

        RuleFor(x => x.DeletedBy)
            .NotEmpty().WithMessage("User ID of the person deleting the request is required.")
            .GreaterThan(0).WithMessage("User ID of the person deleting must be greater than zero.");
    }
}
