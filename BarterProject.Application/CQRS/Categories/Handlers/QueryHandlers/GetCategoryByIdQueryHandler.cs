using AutoMapper;
using BarterProject.Application.CQRS.Categories.Queries.Requests;
using BarterProject.Application.CQRS.Categories.Queries.Responses;
using BarterProject.Common.Exceptions;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Repository.Common;
using MediatR;

namespace BarterProject.Application.CQRS.Categories.Handlers.QueryHandlers;

public class GetCategoryByIdQueryHandler : IRequestHandler<GetCategoryByIdQueryRequest, Result<GetCategoryByIdQueryResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetCategoryByIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<GetCategoryByIdQueryResponse>> Handle(GetCategoryByIdQueryRequest request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.CategoryRepository.GetByIdAsync(request.Id);

        if (category == null)
        {
            //return new Result<GetCategoryByIdQueryResponse>(new List<string> { "Category not found" });
            throw new BadRequestException("Category not found");
        }
        var response = _mapper.Map<GetCategoryByIdQueryResponse>(category);

        return new Result<GetCategoryByIdQueryResponse> { Data = response };
    }
}
