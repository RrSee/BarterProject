using BarterProject.Application.CQRS.Notifications.Queries.Responses;
using MediatR;

namespace BarterProject.Application.CQRS.Notifications.Queries.Requests;

public class GetByIdNotificationQuery : IRequest<GetByIdNotificationResponse>
{
    public int NotificationId { get; set; }

    public GetByIdNotificationQuery(int notificationId)
    {
        NotificationId = notificationId;
    }
}