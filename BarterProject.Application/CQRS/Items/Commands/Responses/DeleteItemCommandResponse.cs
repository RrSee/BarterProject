namespace BarterProject.Application.CQRS.Items.Commands.Responses;

public class DeleteItemCommandResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
}
