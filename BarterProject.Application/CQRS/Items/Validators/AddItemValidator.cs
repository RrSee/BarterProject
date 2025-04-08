using BarterProject.Application.CQRS.Items.Commands.Requests;
using FluentValidation;

namespace BarterProject.Application.CQRS.Items.Validators;

public class AddItemValidator : AbstractValidator<AddItemCommandRequest>
{
    public AddItemValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Item name is required.")
            .MaximumLength(50).WithMessage("Item name must be at most 50 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description must be at most 500 characters.");

        RuleFor(x => x.ImagePath)
            .NotEmpty().WithMessage("Image path is required.")
            //.Matches(@"^(http|https):\/\/").WithMessage("Image path must be a valid URL.")
            .MaximumLength(1000).WithMessage("Image path must be at most 1000 characters.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.")
            .GreaterThan(0).WithMessage("User ID must be greater than zero.");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("CategoryId must be greater than zero.");
    }

}