using BarterProject.Application.CQRS.Notifications.Queries.Responses;
using MediatR;

namespace BarterProject.Application.CQRS.Notifications.Queries.Requests;

public class GetNotificationByIdQuery : IRequest<GetNotificationByIdResponse>
{
    public int NotificationId { get; set; }

    public GetNotificationByIdQuery(int notificationId)
    {
        NotificationId = notificationId;
    }
}