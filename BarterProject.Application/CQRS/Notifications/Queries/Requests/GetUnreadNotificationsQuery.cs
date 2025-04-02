using BarterProject.Application.CQRS.Notifications.Queries.Responses;
using MediatR;

namespace BarterProject.Application.CQRS.Notifications.Queries.Requests;

public class GetUnreadNotificationsQuery : IRequest<List<GetUnreadNotificationsResponse>>
{
    public int UserId { get; set; }

    public GetUnreadNotificationsQuery(int userId)
    {
        UserId = userId;
    }
}

