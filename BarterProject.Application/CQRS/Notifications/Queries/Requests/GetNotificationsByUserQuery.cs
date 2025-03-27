using BarterProject.Application.CQRS.Notifications.Queries.Responses;
using MediatR;

namespace BarterProject.Application.CQRS.Notifications.Queries.Requests;

public class GetNotificationsByUserQuery : IRequest<List<GetNotificationsByUserResponse>>
{
    public int UserId { get; set; }

    public GetNotificationsByUserQuery(int userId)
    {
        UserId = userId;
    }
}
