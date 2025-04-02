using BarterProject.Application.CQRS.Notifications.Commands.Responses;
using MediatR;

namespace BarterProject.Application.CQRS.Notifications.Commands.Requests;

public class DeleteNotificationCommand : IRequest<DeleteNotificationResponse>
{
    public int NotificationId { get; set; }
    public int UserId { get; set; }

    public DeleteNotificationCommand(int notificationId, int userId)
    {
        NotificationId = notificationId;
        UserId = userId;
    }
}
