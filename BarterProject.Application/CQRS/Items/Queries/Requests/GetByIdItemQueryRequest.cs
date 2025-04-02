using BarterProject.Application.CQRS.Items.Queries.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using MediatR;

namespace BarterProject.Application.CQRS.Items.Queries.Requests;

public class GetByIdItemQueryRequest : IRequest<Result<GetByIdItemQueryResponse>>
{
    public int Id { get; set; }
}