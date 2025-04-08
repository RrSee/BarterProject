namespace BarterProject.Application.CQRS.Categories.Command.Responses;

public class UpdateCategoryCommandResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime UpdatedDate { get; set; }
}
