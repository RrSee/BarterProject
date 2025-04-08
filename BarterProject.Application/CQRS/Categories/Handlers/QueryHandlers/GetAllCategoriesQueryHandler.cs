using AutoMapper;
using BarterProject.Application.CQRS.Categories.Queries.Requests;
using BarterProject.Application.CQRS.Categories.Queries.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Repository.Common;
using MediatR;

namespace BarterProject.Application.CQRS.Categories.Handlers.QueryHandlers;

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQueryRequest, Result<List<GetAllCategoriesQueryResponse>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetAllCategoriesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<GetAllCategoriesQueryResponse>>> Handle(GetAllCategoriesQueryRequest request, CancellationToken cancellationToken)
    {
        var categories = await _unitOfWork.CategoryRepository.GetAllAsync();

        if (categories == null || !categories.Any())
        {
            return new Result<List<GetAllCategoriesQueryResponse>>(new List<string> { "No categories found" });
        }
        var response = _mapper.Map<List<GetAllCategoriesQueryResponse>>(categories);
        return new Result<List<GetAllCategoriesQueryResponse>> { Data = response };
    }
}
