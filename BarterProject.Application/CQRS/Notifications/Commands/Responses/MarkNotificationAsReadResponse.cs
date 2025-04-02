namespace BarterProject.Application.CQRS.Notifications.Commands.Responses;

public class MarkNotificationAsReadResponse
{
    public int NotificationId { get; set; }
    public bool IsRead { get; set; }
    public bool IsSuccess { get; set; } 
    public string Message { get; set; } 
}
