using BarterProject.Application.CQRS.Comments.Commands.Requests;
using FluentValidation;

namespace BarterProject.Application.CQRS.Comments.Validators;

public sealed class DeleteCommentValidator : AbstractValidator<DeleteCommentRequest>
{
    public DeleteCommentValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Barter request ID is required.")
            .GreaterThan(0).WithMessage("Barter ID must be greater than zero.");

        RuleFor(x => x.DeletedBy)
            .NotEmpty().WithMessage("User ID of the person deleting the request is required.")
            .GreaterThan(0).WithMessage("User ID of the person deleting must be greater than zero.");
    }
}
