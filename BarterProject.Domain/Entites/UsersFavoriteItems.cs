using BarterProject.Domain.BaseEntities;

namespace BarterProject.Domain.Entites;

public class UsersFavoriteItems : BaseEntity
{
    public int UserId { get; set; }
    public int ItemId { get; set; }
}