using BarterProject.Application.CQRS.Comments.Commands.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using MediatR;

namespace BarterProject.Application.CQRS.Comments.Commands.Requests;

public class CreateCommentRequest : IRequest<Result<CreateCommentResponse>>
{
    public string Description { get; set; }
    public int UserId { get; set; }
    public int ItemId { get; set; }
}
