using BarterProject.Domain.Enums;

namespace BarterProject.Application.CQRS.BarterRequests.Commands.Responses;

public class UpdateBarterRequestStatusResponse
{
    public int RequestId { get; set; }
    public BarterStatus Status { get; set; }
}
