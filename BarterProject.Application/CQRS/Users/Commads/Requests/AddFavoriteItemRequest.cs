using BarterProject.Application.CQRS.Users.Commads.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using MediatR;

namespace BarterProject.Application.CQRS.Users.Commads.Requests;

public class AddFavoriteItemRequest : IRequest<Result<AddFavoriteItemResponse>>
{
    public int UserId { get; set; }
    public int ItemId { get; set; }
}
