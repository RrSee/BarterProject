using BarterProject.Application.CQRS.Notifications.Commands.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using MediatR;

namespace BarterProject.Application.CQRS.Notifications.Commands.Requests;

public class CreateNotificationCommand : IRequest<Result<CreateNotificationResponse>>
{
    public string Description { get; set; } = string.Empty;
    public int SendedUserId { get; set; }
    public int UserId { get; set; }
}