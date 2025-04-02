using BarterProject.Application.CQRS.Users.Commads.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using MediatR;

namespace BarterProject.Application.CQRS.Users.Commads.Requests;

public class UpdateUserRequest : IRequest<Result<UpdateUserResponse>>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Email { get; set; }
    public string Telephone { get; set; }
    public string Address { get; set; }
}
