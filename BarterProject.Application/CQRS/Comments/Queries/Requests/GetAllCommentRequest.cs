using BarterProject.Application.CQRS.Comments.Queries.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using MediatR;

namespace BarterProject.Application.CQRS.Comments.Queries.Requests;

public class GetAllCommentRequest : IRequest<Result<List<GetAllCommentResponse>>>
{
}
