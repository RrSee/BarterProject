using BarterProject.Application.CQRS.Categories.Command.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using MediatR;

namespace BarterProject.Application.CQRS.Categories.Command.Requests;

public class UpdateCategoryCommandRequest : IRequest<Result<UpdateCategoryCommandResponse>>
{
    public int Id { get; set; }
    public string Name { get; set; }
}