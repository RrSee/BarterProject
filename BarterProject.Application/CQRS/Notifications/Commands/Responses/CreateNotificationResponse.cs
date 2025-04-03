namespace BarterProject.Application.CQRS.Notifications.Commands.Responses;
public class CreateNotificationResponse
{
    public int Id { get; set; }
    public string Description { get; set; }
    public int SendedUserId { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedDate { get; set; }

    public bool IsSuccess { get; set; }
    public List<string> ErrorMessages { get; set; } = new List<string>();
}
