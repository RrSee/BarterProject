using BarterProject.Application.CQRS.Users.Queries.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using MediatR;

namespace BarterProject.Application.CQRS.Users.Queries.Requests;

public class GetByIdUserRequest : IRequest<Result<GetByIdUserResponse>>
{
    public int UserId { get; set; }
}
