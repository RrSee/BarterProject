using BarterProject.Application.CQRS.Items.Commands.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using MediatR;


namespace BarterProject.Application.CQRS.Items.Commands.Requests;

public class DeleteItemCommandRequest:IRequest<Result<DeleteItemCommandResponse>>
{
    public int Id { get; set; }
    public int DeletedBy { get; set; }
}
