using BarterProject.Application.CQRS.Users.Queries.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using MediatR;

namespace BarterProject.Application.CQRS.Users.Queries.Requests;

public class GetByEmailUserRequest : IRequest<Result<GetByEmailUserResponse>>
{
    public string Email { get; set; }
}
