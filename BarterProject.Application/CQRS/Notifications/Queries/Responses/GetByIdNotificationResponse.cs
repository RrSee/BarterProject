namespace BarterProject.Application.CQRS.Notifications.Queries.Responses;

public class GetByIdNotificationResponse
{
    public int Id { get; set; }
    public string Description { get; set; }
    public int SendedUserId { get; set; }
    public int UserId { get; set; }
    public bool IsRead { get; set; }
    public DateTime CreatedDate { get; set; }
}