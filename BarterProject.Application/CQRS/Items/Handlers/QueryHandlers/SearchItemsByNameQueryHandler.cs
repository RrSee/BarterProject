using AutoMapper;
using BarterProject.Application.CQRS.Items.Queries.Requests;
using BarterProject.Application.CQRS.Items.Queries.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Domain.Entites;
using BarterProject.Repository.Common;
using MediatR;

namespace BarterProject.Application.CQRS.Items.Handlers.QueryHandlers;
public class SearchItemsByNameQueryHandler : IRequestHandler<SearchItemsByNameQueryRequest, Result<List<SearchItemsByNameQueryResponse>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public SearchItemsByNameQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<Result<List<SearchItemsByNameQueryResponse>>> Handle(SearchItemsByNameQueryRequest request, CancellationToken cancellationToken)
    {
        var keyword = request.Keyword?.Trim().ToLower() ?? "";  
        var items = await _unitOfWork.ItemRepository.GetAllAsync();
        var filteredItems = items.Where(i => i.Name.ToLower().Contains(keyword)).ToList();

        if (!filteredItems.Any())
        {
            return new Result<List<SearchItemsByNameQueryResponse>>(new List<string> { "No items found matching the search term" });
        }
        var response = _mapper.Map<List<SearchItemsByNameQueryResponse>>(filteredItems);

        return new Result<List<SearchItemsByNameQueryResponse>> { IsSuccess = true, Data = response };
    }
}
