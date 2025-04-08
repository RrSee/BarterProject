using BarterProject.Application.CQRS.Items.Commands.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using MediatR;

namespace BarterProject.Application.CQRS.Items.Commands.Requests;

public class UpdateItemCommandRequest : IRequest<Result<UpdateItemCommandResponse>>
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public string? ImagePath { get; set; }
    public int CategoryId { get; set; }
    public int UpdatedBy { get; set; }
}
