using BarterProject.Application.CQRS.Notifications.Queries.Responses;
using MediatR;

namespace BarterProject.Application.CQRS.Notifications.Queries.Requests;

public class GetByUserIdNotificationsQuery : IRequest<List<GetByUserIdNotificationsResponse>>
{
    public int UserId { get; set; }

    public GetByUserIdNotificationsQuery(int userId)
    {
        UserId = userId;
    }
}
