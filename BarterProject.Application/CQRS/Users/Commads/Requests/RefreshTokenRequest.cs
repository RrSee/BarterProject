using BarterProject.Common.GlobalResponses;
using BarterProject.Common.GlobalResponses.Generics;
using MediatR;

namespace BarterProject.Application.CQRS.Users.Commads.Requests;

public class RefreshTokenRequest : IRequest<Result<string>>
{
    public string Token { get; set; }
}