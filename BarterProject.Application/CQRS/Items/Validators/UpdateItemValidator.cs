using BarterProject.Application.CQRS.Items.Commands.Requests;
using BarterProject.Application.CQRS.Items.Commands.Responses;
using BarterProject.Common.GlobalResponses;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Repository.Repositories;
using FluentValidation;
using MediatR;

namespace BarterProject.Application.CQRS.Items.Validators;

public class UpdateItemValidator : AbstractValidator<UpdateItemCommandRequest>
{
    public UpdateItemValidator()
    {
        RuleFor(x => x.Id)
            .GreaterThan(0).WithMessage("Item ID must be greater than zero.")
            .NotEmpty().WithMessage("Item ID is required.");

        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Item name is required.")
            .MaximumLength(100).WithMessage("Item name must be at most 100 characters.");

        RuleFor(x => x.Description)
            .NotEmpty().WithMessage("Description is required.")
            .MaximumLength(500).WithMessage("Description must be at most 500 characters.");

        RuleFor(x => x.ImagePath)
            .Matches(@"^(http|https):\/\/").WithMessage("Image path must be a valid URL.")
            .When(x => !string.IsNullOrEmpty(x.ImagePath))
            .WithMessage("Image path must be a valid URL if provided.");

        RuleFor(x => x.UpdatedBy)
            .NotEmpty().WithMessage("UpdatedBy is required.")
            .GreaterThan(0).WithMessage("UpdatedBy must be greater than zero.");
    }
}

