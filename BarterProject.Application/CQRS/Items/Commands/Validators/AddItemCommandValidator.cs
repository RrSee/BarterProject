using BarterProject.Application.CQRS.Items.Commands.Requests;
using FluentValidation;

namespace BarterProject.Application.CQRS.Items.Commands.Validators;

public class AddItemCommandValidator : AbstractValidator<AddItemCommandRequest>
{
    public AddItemCommandValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Item name is required.")
            .MaximumLength(100).WithMessage("Item name must be at most 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description must be at most 500 characters.");

        RuleFor(x => x.ImagePath)
            .NotEmpty().WithMessage("Image path is required.")
            .Matches(@"^(http|https):\/\/").WithMessage("Image path must be a valid URL.");

        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("User ID must be greater than zero.");
    }
}