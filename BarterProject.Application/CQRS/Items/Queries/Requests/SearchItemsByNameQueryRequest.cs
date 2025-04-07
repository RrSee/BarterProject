using BarterProject.Application.CQRS.Items.Queries.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using MediatR;

namespace BarterProject.Application.CQRS.Items.Queries.Requests;

public class SearchItemsByNameQueryRequest : IRequest<Result<List<SearchItemsByNameQueryResponse>>>
{
    public string Keyword { get; set; }
}
