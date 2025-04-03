using BarterProject.Application.CQRS.Comments.Commands.Requests;
using FluentValidation;

namespace BarterProject.Application.CQRS.Comments.Commands.Validators;

public class CreateCommentRequestValidator : AbstractValidator<CreateCommentRequest>
{
    public CreateCommentRequestValidator()
    {
        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description must be at most 500 characters.");

        RuleFor(x => x.UserId)
            .GreaterThan(0).WithMessage("User ID must be greater than zero.");

        RuleFor(x => x.ItemId)
            .GreaterThan(0).WithMessage("Item ID must be greater than zero.");
    }
}
