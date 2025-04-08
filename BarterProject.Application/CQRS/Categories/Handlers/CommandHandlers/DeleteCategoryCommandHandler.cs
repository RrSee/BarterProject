using BarterProject.Application.CQRS.Categories.Command.Requests;
using BarterProject.Application.CQRS.Categories.Command.Responses;
using BarterProject.Common.Exceptions;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Repository.Common;
using MediatR;

namespace BarterProject.Application.CQRS.Categories.Handlers.CommandHandlers;

public class DeleteCategoryCommandHandler : IRequestHandler<DeleteCategoryCommandRequest, Result<DeleteCategoryCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<DeleteCategoryCommandResponse>> Handle(DeleteCategoryCommandRequest request, CancellationToken cancellationToken)
    {

        var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.Id);


        if (category == null)
        {
            throw new BadRequestException("Category not found.");
        }

        category.IsDeleted = true;
        category.DeletedBy = request.DeletedBy;
        category.DeletedDate = DateTime.Now;
        var isDeleted = await _unitOfWork.CategoryRepository.UpdateAsync(category);
        await _unitOfWork.CommitAsync();

        if (!isDeleted)
        {
            throw new BadRequestException("Error occurred while deleting the category.");
        }
        var response = new DeleteCategoryCommandResponse
        {
            Id = category.Id,
            
        };

        return new Result<DeleteCategoryCommandResponse> { IsSuccess = true, Data = response };
    }
}

