using BarterProject.Application.CQRS.Comments.Queries.Requests;
using FluentValidation;

namespace BarterProject.Application.CQRS.Comments.Validators;

public sealed class GetByItemIdCommentValidator : AbstractValidator<GetByItemIdCommentRequest>
{
    public GetByItemIdCommentValidator()
    {
        RuleFor(x => x.ItemId)
            .NotEmpty().WithMessage("Item ID is required.")
            .GreaterThan(0).WithMessage("Item ID must be greater than zero.");
    }
}
