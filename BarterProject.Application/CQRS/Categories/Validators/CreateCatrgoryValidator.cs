using BarterProject.Application.CQRS.Categories.Command.Requests;
using FluentValidation;

namespace BarterProject.Application.CQRS.Categories.Validators;

public class CreateCatrgoryValidator : AbstractValidator<CreateCategoryCommandRequest>
{
    public CreateCatrgoryValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Category name is required.")
            .MaximumLength(50)
            .WithMessage("Category name must not exceed 50 characters.");
    }
}