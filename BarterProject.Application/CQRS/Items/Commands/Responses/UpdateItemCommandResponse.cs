namespace BarterProject.Application.CQRS.Items.Commands.Responses;

public class UpdateItemCommandResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? ImagePath { get; set; }
    public int CategorId { get; set; }
    public int UpdatedBy { get; set; }
    public DateTime UpdatedDate { get; set; }
}