using BarterProject.Application.CQRS.Comments.Commands.Requests;
using FluentValidation;

namespace BarterProject.Application.CQRS.Comments.Commands.Validators;

public class UpdateCommentRequestValidator : AbstractValidator<UpdateCommentRequest>
{
    public UpdateCommentRequestValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Comment ID must be greater than zero.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description must be at most 500 characters.");
    }
}