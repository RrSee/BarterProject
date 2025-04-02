using BarterProject.Application.CQRS.Notifications.Commands.Responses;
using MediatR;

namespace BarterProject.Application.CQRS.Notifications.Commands.Requests;

public class CreateNotificationCommand : IRequest<CreateNotificationResponse>
{
    public string Description { get; set; } = string.Empty;
    public int SendedUserId { get; set; }
    public int UserId { get; set; }
}