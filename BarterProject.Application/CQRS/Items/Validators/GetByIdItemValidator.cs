using BarterProject.Application.CQRS.Items.Queries.Requests;
using FluentValidation;

namespace BarterProject.Application.CQRS.Items.Validators;

public sealed class GetByIdItemValidator : AbstractValidator<GetByIdItemQueryRequest>
{
    public GetByIdItemValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty().WithMessage("Item ID is required.")
            .GreaterThan(0).WithMessage("Item ID must be greater than zero.");
    }
}
