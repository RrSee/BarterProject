using BarterProject.Application.CQRS.Users.Commads.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using MediatR;

namespace BarterProject.Application.CQRS.Users.Commads.Requests;

public class DeleteUserRequest : IRequest<Result<DeleteUserResponse>>
{
    public int Id { get; set; }
    public int DeletedBy { get; set; }
}
