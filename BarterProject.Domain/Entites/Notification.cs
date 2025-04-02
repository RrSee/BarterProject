using BarterProject.Domain.BaseEntities;

namespace BarterProject.Domain.Entites;

public class Notification:BaseEntity
{
    public string Description { get; set; } = string.Empty;
    public int SendedUserId { get; set; } // Bildirişi göndərən user
    public int UserId { get; set; } // Bildirişi alan user
    public bool IsRead { get; set; } = false;
}
