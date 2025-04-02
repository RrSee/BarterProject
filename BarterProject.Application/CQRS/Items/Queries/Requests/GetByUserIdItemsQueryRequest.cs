using BarterProject.Application.CQRS.Items.Queries.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using MediatR;

namespace BarterProject.Application.CQRS.Items.Queries.Requests;

public class GetByUserIdItemsQueryRequest : IRequest<Result<List<GetByUserIdItemsQueryResponse>>>
{
    public int UserId { get; set; }
}