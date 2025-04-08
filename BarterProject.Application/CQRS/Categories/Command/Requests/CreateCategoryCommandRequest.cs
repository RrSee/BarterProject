using BarterProject.Application.CQRS.Categories.Command.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Domain.Entites;
using MediatR;

namespace BarterProject.Application.CQRS.Categories.Command.Requests;

public class CreateCategoryCommandRequest : IRequest<Result<CreateCategoryCommandResponse>>
{
    public string Name { get; set; }
}