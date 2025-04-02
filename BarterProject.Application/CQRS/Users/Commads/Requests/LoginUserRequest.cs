using BarterProject.Application.CQRS.Users.Commads.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using MediatR;

namespace BarterProject.Application.CQRS.Users.Commads.Requests;

public class LoginUserRequest : IRequest<Result<LoginUserResponse>>
{
    public string Email { get; set; }
    public string Password { get; set; }
}
