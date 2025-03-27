namespace BarterProject.Application.CQRS.Notifications.Commands.Responses;

public class UpdateNotificationResponse
{
    public bool IsSuccess { get; set; }
    public string Message { get; set; }
}