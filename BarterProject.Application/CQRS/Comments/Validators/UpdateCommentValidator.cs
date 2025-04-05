using BarterProject.Application.CQRS.Comments.Commands.Requests;
using FluentValidation;

namespace BarterProject.Application.CQRS.Comments.Validators;

public class UpdateCommentValidator : AbstractValidator<UpdateCommentRequest>
{
    public UpdateCommentValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Comment ID is required.")
            .GreaterThan(0).WithMessage("Comment ID must be greater than zero.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description must be at most 500 characters.");
    }
}