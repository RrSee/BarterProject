using BarterProject.Application.CQRS.Items.Commands.Requests;
using FluentValidation;

namespace BarterProject.Application.CQRS.Items.Validators;

public sealed class DeleteItemValidator : AbstractValidator<DeleteItemCommandRequest>
{
    public DeleteItemValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Item ID is required.")
            .GreaterThan(0).WithMessage("Item ID must be greater than zero.");

        RuleFor(x => x.DeletedBy)
            .NotEmpty().WithMessage("User ID of the person deleting the item is required.")
            .GreaterThan(0).WithMessage("User ID of the person deleting must be greater than zero.");
    }
}
