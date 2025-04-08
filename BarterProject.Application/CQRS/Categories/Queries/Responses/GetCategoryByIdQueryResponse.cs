namespace BarterProject.Application.CQRS.Categories.Queries.Responses;

public class GetCategoryByIdQueryResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime CreatedDate { get; set; }
}
