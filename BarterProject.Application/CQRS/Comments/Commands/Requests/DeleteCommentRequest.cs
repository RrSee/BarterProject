using BarterProject.Application.CQRS.Comments.Commands.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using MediatR;

namespace BarterProject.Application.CQRS.Comments.Commands.Requests;

public class DeleteCommentRequest : IRequest<Result<DeleteCommentResponse>>
{
    public int Id { get; set; }
    public int DeletedBy { get; set; }
}
