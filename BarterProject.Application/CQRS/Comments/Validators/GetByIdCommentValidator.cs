using BarterProject.Application.CQRS.Comments.Queries.Requests;
using FluentValidation;

namespace BarterProject.Application.CQRS.Comments.Validators;

public sealed class GetByIdCommentValidator : AbstractValidator<GetByIdCommentRequest>
{
    public GetByIdCommentValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Comment ID is required.")
            .GreaterThan(0).WithMessage("Comment ID must be greater than zero.");
    }
}