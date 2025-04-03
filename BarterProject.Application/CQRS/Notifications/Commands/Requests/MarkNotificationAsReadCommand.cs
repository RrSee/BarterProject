using BarterProject.Application.CQRS.Notifications.Commands.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using MediatR;

namespace BarterProject.Application.CQRS.Notifications.Commands.Requests;

public class MarkNotificationAsReadCommand : IRequest<Result<MarkNotificationAsReadResponse>>
{
    public int NotificationId { get; set; }

    public MarkNotificationAsReadCommand(int notificationId)
    {
        NotificationId = notificationId;
    }
}
