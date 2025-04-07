using BarterProject.Application.CQRS.BarterRequests.Commands.Responses;
using BarterProject.Application.CQRS.Users.Commads.Requests;
using BarterProject.Application.CQRS.Users.Commads.Responses;
using BarterProject.Common.Exceptions;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Repository.Common;
using MediatR;

namespace BarterProject.Application.CQRS.Users.Handlers.CommandHandlers;

public class DeleteUserHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteUserRequest, Result<DeleteUserResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<DeleteUserResponse>> Handle(DeleteUserRequest request, CancellationToken cancellationToken)
    {
        var success = await _unitOfWork.UserRepository.Remove(request.Id, request.DeletedBy);

        if (success == false)
        {
            throw new BadRequestException("User not found or already deleted.");
        }

        return new Result<DeleteUserResponse>
        {
            Data = new DeleteUserResponse { IsDeleted = true },
            IsSuccess = success,
            Errors = []
        };
    }
}
