using BarterProject.Application.CQRS.Comments.Commands.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using MediatR;

namespace BarterProject.Application.CQRS.Comments.Commands.Requests;

public class UpdateCommentRequest : IRequest<Result<UpdateCommentResponse>>
{
    public int Id { get; set; }
    public string Description { get; set; }
}
