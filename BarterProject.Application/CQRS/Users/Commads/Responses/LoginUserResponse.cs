namespace BarterProject.Application.CQRS.Users.Commads.Responses;

public class LoginUserResponse
{
    public string AccessToken { get; set; }
    public string RefreshToken { get; set; }
}
