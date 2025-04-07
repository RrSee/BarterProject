using AutoMapper;
using BarterProject.Application.CQRS.Items.Commands.Requests;
using BarterProject.Application.CQRS.Items.Commands.Responses;
using BarterProject.Common.Exceptions;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Repository.Common;
using MediatR;

namespace BarterProject.Application.CQRS.Items.Handlers.CommandsHandlers;

public class DeleteItemCommandHandler : IRequestHandler<DeleteItemCommandRequest, Result<DeleteItemCommandResponse>>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteItemCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<DeleteItemCommandResponse>> Handle(DeleteItemCommandRequest request, CancellationToken cancellationToken)
    {
        var item = await _unitOfWork.ItemRepository.GetByIdAsync(request.Id);

        if (item == null)
        {
            //return new Result<DeleteItemCommandResponse>(new List<string> { "Item not found" });
            throw new BadRequestException("Item not found.");

        }
        var isDeleted = await _unitOfWork.ItemRepository.DeleteAsync(request.Id, request.DeletedBy);

        if (!isDeleted)
        {
            //return new Result<DeleteItemCommandResponse>(new List<string> { "Error occurred while deleting the item" });
            throw new BadRequestException("Error occurred while deleting the item");

        }
        return new Result<DeleteItemCommandResponse> { IsSuccess = true };
    }
}

