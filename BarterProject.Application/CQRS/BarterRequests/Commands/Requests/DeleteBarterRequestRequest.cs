using BarterProject.Application.CQRS.BarterRequests.Commands.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using MediatR;

namespace BarterProject.Application.CQRS.BarterRequests.Commands.Requests;

public class DeleteBarterRequestRequest : IRequest<Result<DeleteBarterRequestResponse>>
{
    public int Id { get; set; }
    public int DeletedBy { get; set; }
}
