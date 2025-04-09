using AutoMapper;
using BarterProject.Application.CQRS.Categories.Command.Requests;
using BarterProject.Application.CQRS.Categories.Command.Responses;
using BarterProject.Common.Exceptions;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Repository.Common;
using MediatR;

namespace BarterProject.Application.CQRS.Categories.Handlers.CommandHandlers;

public class UpdateCategoryCommandHandler : IRequestHandler<UpdateCategoryCommandRequest, Result<UpdateCategoryCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<UpdateCategoryCommandResponse>> Handle(UpdateCategoryCommandRequest request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.Id);

        if (category == null)
        {
            //return new Result<UpdateCategoryCommandResponse>(new List<string> { "Category not found" });
            throw new BadRequestException("Category not found");
        }

        category.Name = request.Name;
        category.UpdatedDate = DateTime.Now;

        var updatedCategory = await _unitOfWork.CategoryRepository.UpdateAsync(category);
        await _unitOfWork.CommitAsync();

        if (!updatedCategory)
        {
            //return new Result<UpdateCategoryCommandResponse>(new List<string> { "Category update failed" });
            throw new BadRequestException("Category update failed");
        }
        var response = _mapper.Map<UpdateCategoryCommandResponse>(category);
        return new Result<UpdateCategoryCommandResponse> { Data = response };
    }
}
