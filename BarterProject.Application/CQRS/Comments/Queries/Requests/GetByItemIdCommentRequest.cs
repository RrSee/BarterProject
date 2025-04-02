using BarterProject.Application.CQRS.Comments.Queries.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using MediatR;

namespace BarterProject.Application.CQRS.Comments.Queries.Requests;

public class GetByItemIdCommentRequest : IRequest<Result<List<GetByItemIdCommentResponse>>>
{
    public int ItemId { get; set; }
}
