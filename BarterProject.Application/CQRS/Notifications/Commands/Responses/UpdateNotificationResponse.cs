namespace BarterProject.Application.CQRS.Notifications.Commands.Responses;

public class UpdateNotificationResponse
{
    public bool IsSuccess { get; set; }
    public List<string> ErrorMessages { get; set; } = new List<string>();
}