using BarterProject.Application.CQRS.BarterRequests.Commands.Responses;
using BarterProject.Common.GlobalResponses;
using BarterProject.Common.GlobalResponses.Generics;
using MediatR;

namespace BarterProject.Application.CQRS.BarterRequests.Commands.Requests;

public class CreateBarterRequestRequest : IRequest<Result<CreateBarterRequestResponse>>
{
    public int SenderUserId { get; set; }
    public int ReceiverUserId { get; set; }
    public int SenderItemId { get; set; }
    public int ReceiverItemId { get; set; }
}
