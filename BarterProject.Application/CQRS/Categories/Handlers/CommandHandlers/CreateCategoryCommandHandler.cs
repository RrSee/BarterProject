using AutoMapper;
using BarterProject.Application.CQRS.Categories.Command.Requests;
using BarterProject.Application.CQRS.Categories.Command.Responses;
using BarterProject.Common.Exceptions;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Domain.Entites;
using BarterProject.Repository.Common;
using MediatR;

namespace BarterProject.Application.CQRS.Categories.Handlers.CommandHandlers;

public class CreateCategoryCommandHandler : IRequestHandler<CreateCategoryCommandRequest, Result<CreateCategoryCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<CreateCategoryCommandResponse>> Handle(CreateCategoryCommandRequest request, CancellationToken cancellationToken)
    {
        var category = new Category
        {
            Name = request.Name,
            CreatedDate = DateTime.Now
        };

        var createdCategory = await _unitOfWork.CategoryRepository.AddAsync(category);
        await _unitOfWork.CommitAsync();
        if (createdCategory == null)
        {
            //return new Result<CreateCategoryCommandResponse>(new List<string> { "Category creation failed" });
            throw new BadRequestException("Category creation failed");
        }

        var response = _mapper.Map<CreateCategoryCommandResponse>(createdCategory);
        return new Result<CreateCategoryCommandResponse> { Data = response };
    }
}


