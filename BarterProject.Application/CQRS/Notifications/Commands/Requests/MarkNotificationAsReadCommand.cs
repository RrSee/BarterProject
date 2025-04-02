using BarterProject.Application.CQRS.Notifications.Commands.Responses;
using MediatR;

namespace BarterProject.Application.CQRS.Notifications.Commands.Requests;

public class MarkNotificationAsReadCommand : IRequest<MarkNotificationAsReadResponse>
{
    public int NotificationId { get; set; }

    public MarkNotificationAsReadCommand(int notificationId)
    {
        NotificationId = notificationId;
    }
}
