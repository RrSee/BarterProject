using BarterProject.Application.CQRS.Notifications.Queries.Responses;
using MediatR;

namespace BarterProject.Application.CQRS.Notifications.Queries.Requests;

public class GetAllNotificationsQuery : IRequest<List<GetAllNotificationsResponse>>
{
}