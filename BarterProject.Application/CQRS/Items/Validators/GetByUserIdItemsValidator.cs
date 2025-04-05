using BarterProject.Application.CQRS.Items.Queries.Requests;
using FluentValidation;

namespace BarterProject.Application.CQRS.Items.Validators;

public sealed class GetByUserIdItemsValidator : AbstractValidator<GetByUserIdItemsQueryRequest>
{
    public GetByUserIdItemsValidator()
    {
        RuleFor(x => x.UserId)
            .NotEmpty().WithMessage("User ID is required.")
            .GreaterThan(0).WithMessage("User ID must be greater than zero.");
    }
}
