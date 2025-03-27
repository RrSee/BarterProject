using BarterProject.Domain.BaseEntities;

namespace BarterProject.Domain.Entites;

public class BarterRequest : BaseEntity
{
    public int SenderUserId { get; set; }
    public int ReceiverUserId { get; set; }
    public int SenderItemId { get; set; }
    public int ReceiverItemId { get; set; }
    public string Status { get; set; } = "Pending";
}