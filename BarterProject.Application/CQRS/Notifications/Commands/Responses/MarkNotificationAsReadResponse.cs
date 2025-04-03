namespace BarterProject.Application.CQRS.Notifications.Commands.Responses;

public class MarkNotificationAsReadResponse
{
    public int NotificationId { get; set; }
    public bool IsRead { get; set; }
}
