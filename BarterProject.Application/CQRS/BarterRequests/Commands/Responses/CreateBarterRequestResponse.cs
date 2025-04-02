namespace BarterProject.Application.CQRS.BarterRequests.Commands.Responses;

public class CreateBarterRequestResponse
{
    public int SenderUserId { get; set; }
    public int ReceiverUserId { get; set; }
    public int SenderItemId { get; set; }
    public int ReceiverItemId { get; set; }
}
