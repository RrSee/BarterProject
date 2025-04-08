using AutoMapper;
using BarterProject.Application.CQRS.Items.Queries.Requests;
using BarterProject.Application.CQRS.Items.Queries.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Repository.Common;
using MediatR;

namespace BarterProject.Application.CQRS.Items.Handlers.QueryHandlers;
public class GetItemsByCategoryIdQueryHandler : IRequestHandler<GetItemsByCategoryIdQueryRequest, Result<List<GetItemsByCategoryIdQueryResponse>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public GetItemsByCategoryIdQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<GetItemsByCategoryIdQueryResponse>>> Handle(GetItemsByCategoryIdQueryRequest request, CancellationToken cancellationToken)
    {
        
        var items = await _unitOfWork.ItemRepository.GetByCategoryIdAsync(request.CategoryId);

        if (items == null || !items.Any())
        {
            return new Result<List<GetItemsByCategoryIdQueryResponse>>(new List<string> { "No items found for the given CategoryId" });
        }

        var response = _mapper.Map<List<GetItemsByCategoryIdQueryResponse>>(items);

        return new Result<List<GetItemsByCategoryIdQueryResponse>> { IsSuccess = true, Data = response };
    }
}
