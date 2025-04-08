using AutoMapper;
using BarterProject.Application.CQRS.Items.Queries.Requests;
using BarterProject.Application.CQRS.Items.Queries.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Repository.Common;
using MediatR;

namespace BarterProject.Application.CQRS.Items.Handlers.QueryHandlers;

public class SearchItemsByCategoryAndNameQueryHandler : IRequestHandler<SearchItemsByCategoryAndNameQueryRequest, Result<List<SearchItemsByCategoryAndNameQueryResponse>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SearchItemsByCategoryAndNameQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<SearchItemsByCategoryAndNameQueryResponse>>> Handle(SearchItemsByCategoryAndNameQueryRequest request, CancellationToken cancellationToken)
    {
        var items = await _unitOfWork.ItemRepository.GetAllAsync();

        if (request.CategoryId.HasValue)
        {
            items = items.Where(i => i.CategoryId == request.CategoryId.Value).ToList();
        }

        if (!string.IsNullOrWhiteSpace(request.Keyword))
        {
            items = items.Where(i => i.Name.ToLower().Contains(request.Keyword.ToLower())).ToList();
        }

        if (items == null || !items.Any())
        {
            return new Result<List<SearchItemsByCategoryAndNameQueryResponse>>(new List<string> { "No items found matching the search criteria." });
        }
        var response = _mapper.Map<List<SearchItemsByCategoryAndNameQueryResponse>>(items);

        return new Result<List<SearchItemsByCategoryAndNameQueryResponse>> { IsSuccess = true, Data = response };
    }
}
