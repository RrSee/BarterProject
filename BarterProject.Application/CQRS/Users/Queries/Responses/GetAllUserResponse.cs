namespace BarterProject.Application.CQRS.Users.Queries.Responses;

public class GetAllUserResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Telephone { get; set; }
    public string Address { get; set; }
}
