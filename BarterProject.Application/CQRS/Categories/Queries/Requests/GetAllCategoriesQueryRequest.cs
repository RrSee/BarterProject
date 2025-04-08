using BarterProject.Application.CQRS.Categories.Queries.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using MediatR;

namespace BarterProject.Application.CQRS.Categories.Queries.Requests;

public class GetAllCategoriesQueryRequest : IRequest<Result<List<GetAllCategoriesQueryResponse>>>
{
    
}
