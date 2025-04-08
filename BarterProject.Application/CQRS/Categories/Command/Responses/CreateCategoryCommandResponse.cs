namespace BarterProject.Application.CQRS.Categories.Command.Responses;

public class CreateCategoryCommandResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedDate { get; set; }
}
