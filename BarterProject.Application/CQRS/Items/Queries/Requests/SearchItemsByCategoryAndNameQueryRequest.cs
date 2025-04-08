using BarterProject.Application.CQRS.Items.Queries.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using MediatR;

namespace BarterProject.Application.CQRS.Items.Queries.Requests;

public class SearchItemsByCategoryAndNameQueryRequest : IRequest<Result<List<SearchItemsByCategoryAndNameQueryResponse>>>
{
    public int? CategoryId { get; set; }
    public string? Keyword { get; set; }

    public SearchItemsByCategoryAndNameQueryRequest(int? categoryId, string? keyword)
    {
        CategoryId = categoryId;
        Keyword = keyword;
    }
}
