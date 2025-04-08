using BarterProject.Application.CQRS.Items.Queries.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using MediatR;

namespace BarterProject.Application.CQRS.Items.Queries.Requests;

public class GetItemsByCategoryIdQueryRequest : IRequest<Result<List<GetItemsByCategoryIdQueryResponse>>>
{
    public int CategoryId { get; set; }

    public GetItemsByCategoryIdQueryRequest(int categoryId)
    {
        CategoryId = categoryId;
    }
}