using BarterProject.Application.CQRS.Comments.Commands.Requests;
using FluentValidation;

namespace BarterProject.Application.CQRS.Comments.Validators;

public class CreateCommentValidator : AbstractValidator<CreateCommentRequest>
{
    public CreateCommentValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(300).WithMessage("Description must be at most 300 characters.");

        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.")
            .GreaterThan(0).WithMessage("User ID must be greater than zero.");

        RuleFor(x => x.ItemId)
            .NotEmpty().WithMessage("Item ID is required.")
            .GreaterThan(0).WithMessage("Item ID must be greater than zero.");
    }
}
