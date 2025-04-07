using BarterProject.Application.CQRS.Items.Commands.Requests;
using BarterProject.Common.Exceptions;
using BarterProject.Common.GlobalResponses;
using BarterProject.Repository.Common;
using FluentValidation;
using MediatR;

namespace BarterProject.Application.CQRS.Items.Handlers.CommandsHandlers;
public class UpdateItemCommandHandler : IRequestHandler<UpdateItemCommandRequest, Result>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateItemCommandRequest> _validator;

    public UpdateItemCommandHandler(IUnitOfWork unitOfWork, IValidator<UpdateItemCommandRequest> validator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result> Handle(UpdateItemCommandRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new Result(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }
        var item = await _unitOfWork.ItemRepository.GetByIdAsync(request.Id);
        if (item == null)
        {
            //return new Result(new List<string> { "Item not found." });
            throw new BadRequestException("Item not found.");

        }

        item.Name = request.Name;
        item.Description = request.Description;
        item.ImagePath = request.ImagePath ?? item.ImagePath;
        item.UpdatedBy = request.UpdatedBy;
        item.UpdatedDate = DateTime.Now;

        await _unitOfWork.ItemRepository.UpdateAsync(item);
        await _unitOfWork.CommitAsync();

        return new Result();
    }
}
