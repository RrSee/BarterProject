using BarterProject.Application.CQRS.Categories.Command.Requests;
using FluentValidation;

namespace BarterProject.Application.CQRS.Categories.Validators;

public class DeleteCategoryValidator : AbstractValidator<DeleteCategoryCommandRequest>
{
    public DeleteCategoryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Category ID is required.")
            .GreaterThan(0)
            .WithMessage("Category ID must be greater than zero.");

        RuleFor(x => x.DeletedBy)
            .NotEmpty()
            .WithMessage("DeletedBy is required.")
            .GreaterThan(0)
            .WithMessage("DeletedBy must be greater than zero.");
    }
}
