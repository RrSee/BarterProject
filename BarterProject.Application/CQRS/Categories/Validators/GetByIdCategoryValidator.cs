using BarterProject.Application.CQRS.Categories.Queries.Requests;
using FluentValidation;

namespace BarterProject.Application.CQRS.Categories.Validators;

public class GetByIdCategoryValidator : AbstractValidator<GetCategoryByIdQueryRequest>
{
    public GetByIdCategoryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Category ID is required.")
            .GreaterThan(0)
            .WithMessage("Category ID must be greater than zero.");
    }
}
