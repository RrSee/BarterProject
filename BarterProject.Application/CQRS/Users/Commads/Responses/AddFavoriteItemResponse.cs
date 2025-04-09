namespace BarterProject.Application.CQRS.Users.Commads.Responses;

public class AddFavoriteItemResponse
{
    public int UserId { get; set; }
    public int ItemId { get; set; }
}
