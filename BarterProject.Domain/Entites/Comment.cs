using BarterProject.Domain.BaseEntities;

namespace BarterProject.Domain.Entites;

public class Comment : BaseEntity
{
    public string Description { get; set; }
    public int UserId { get; set; }
    public int ItemId { get; set; }
}
