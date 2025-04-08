using BarterProject.Application.CQRS.Categories.Queries.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using MediatR;

namespace BarterProject.Application.CQRS.Categories.Queries.Requests;

public class GetCategoryByIdQueryRequest : IRequest<Result<GetCategoryByIdQueryResponse>>
{
    public int Id { get; set; }

    public GetCategoryByIdQueryRequest(int id)
    {
        Id = id;
    }
}
