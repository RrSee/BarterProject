using BarterProject.Application.CQRS.Items.Commands.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using MediatR;

namespace BarterProject.Application.CQRS.Items.Commands.Requests;

public class AddItemCommandRequest : IRequest<Result<AddItemCommandResponse>>
{
    public string Name { get; set; }
    public string Description { get; set; }
    public string ImagePath { get; set; }
    public int UserId { get; set; }
}