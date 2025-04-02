using BarterProject.Domain.BaseEntities;

namespace BarterProject.Domain.Entites;

public class Item:BaseEntity
{
    public string Name { get; set; } = null!;
    public string? ImagePath { get; set; }
    public string Description { get; set; } = null!;
    public int? UserId { get; set; }
    public User? User { get; set; }
}
