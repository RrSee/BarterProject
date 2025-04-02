namespace BarterProject.Application.CQRS.Comments.Commands.Responses;

public class CreateCommentResponse
{
    public string Description { get; set; }
    public int UserId { get; set; }
    public int ItemId { get; set; }
}
