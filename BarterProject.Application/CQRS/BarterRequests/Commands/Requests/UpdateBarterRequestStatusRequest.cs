using BarterProject.Application.CQRS.BarterRequests.Commands.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Domain.Enums;
using MediatR;

namespace BarterProject.Application.CQRS.BarterRequests.Commands.Requests;

public class UpdateBarterRequestStatusRequest : IRequest<Result<UpdateBarterRequestStatusResponse>>
{
    public int RequestId { get; set; }
    public BarterStatus Status { get; set; }
}
