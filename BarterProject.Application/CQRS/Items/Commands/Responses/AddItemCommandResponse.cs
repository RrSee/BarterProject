namespace BarterProject.Application.CQRS.Items.Commands.Responses;

public class AddItemCommandResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int CategoryId { get; set; }
    public string? ImagePath { get; set; }
    public int UserId { get; set; }
    public DateTime CreatedDate { get; set; }
}