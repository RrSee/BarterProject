using BarterProject.Domain.BaseEntities;

namespace BarterProject.Domain.Entites;

public class Category:BaseEntity
{
    public string Name { get; set; }
    public ICollection<Item> Items { get; set; }
}
