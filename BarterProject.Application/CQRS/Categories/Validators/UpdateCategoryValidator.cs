﻿using BarterProject.Application.CQRS.Categories.Command.Requests;
using FluentValidation;

namespace BarterProject.Application.CQRS.Categories.Validators;

public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryCommandRequest>
{
    public UpdateCategoryValidator()
    {
        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Category ID is required.")
            .GreaterThan(0)
            .WithMessage("Category ID must be greater than zero.");

        RuleFor(x => x.Name)
            .NotEmpty()
            .WithMessage("Category name is required.")
            .MaximumLength(50)
            .WithMessage("Category name must not exceed 50 characters.");
    }
}
