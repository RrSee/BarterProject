namespace BarterProject.Application.CQRS.Users.Commads.Responses;

public class UpdateUserResponse
{
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Telephone { get; set; }
    public string Address { get; set; }
}
