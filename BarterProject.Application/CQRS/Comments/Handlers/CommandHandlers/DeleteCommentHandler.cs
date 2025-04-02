using BarterProject.Application.CQRS.Comments.Commands.Requests;
using BarterProject.Application.CQRS.Comments.Commands.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Repository.Common;
using MediatR;

namespace BarterProject.Application.CQRS.Comments.Handlers.CommandHandlers;

public class DeleteCommentHandler(IUnitOfWork unitOfWork) : IRequestHandler<DeleteCommentRequest, Result<DeleteCommentResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<DeleteCommentResponse>> Handle(DeleteCommentRequest request, CancellationToken cancellationToken)
    {
        var success = await _unitOfWork.CommentRepository.Delete(request.Id,request.DeletedBy);

        if (success == false)
        {
            throw new Exception("Customer not found or already deleted."); //------------------------------
        }

        return new Result<DeleteCommentResponse>
        {
            Data = new DeleteCommentResponse { IsDeleted = success },
            IsSuccess = success,
            Errors = []
        };
    }
}
