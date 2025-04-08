using BarterProject.Application.CQRS.Categories.Command.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using MediatR;

namespace BarterProject.Application.CQRS.Categories.Command.Requests;

public class DeleteCategoryCommandRequest : IRequest<Result<DeleteCategoryCommandResponse>>
{
    public int Id { get; set; }
    public int DeletedBy { get; set; }
}
