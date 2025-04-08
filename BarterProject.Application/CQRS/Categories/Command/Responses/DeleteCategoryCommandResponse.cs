namespace BarterProject.Application.CQRS.Categories.Command.Responses;

public class DeleteCategoryCommandResponse
{
    public int Id { get; set; }
    public int DeletedBy { get; set; }
}
