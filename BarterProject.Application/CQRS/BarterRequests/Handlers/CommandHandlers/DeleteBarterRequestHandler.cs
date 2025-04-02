using BarterProject.Application.CQRS.BarterRequests.Commands.Requests;
using BarterProject.Application.CQRS.BarterRequests.Commands.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Repository.Common;
using MediatR;

namespace BarterProject.Application.CQRS.BarterRequests.Handlers.CommandHandlers;

public class DeleteBarterRequestHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteBarterRequestRequest, Result<DeleteBarterRequestResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    public async Task<Result<DeleteBarterRequestResponse>> Handle(DeleteBarterRequestRequest request, CancellationToken cancellationToken)
    {
        var success = await _unitOfWork.BarterRequestRepository.Delete(request.Id,request.DeletedBy);

        if (success == false)
        {
            throw new Exception("Customer not found or already deleted."); //---------------------------------
        }

        return new Result<DeleteBarterRequestResponse>
        {
            Data = new DeleteBarterRequestResponse { IsDeleted = true },
            IsSuccess = success,
            Errors = []
        };
    }
}