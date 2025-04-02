using BarterProject.Application.CQRS.Comments.Commands.Requests;
using BarterProject.Application.CQRS.Comments.Commands.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Repository.Common;
using MediatR;

namespace BarterProject.Application.CQRS.Comments.Handlers.CommandHandlers;

public class UpdateCommentHandler(IUnitOfWork unitOfWork) : IRequestHandler<UpdateCommentRequest, Result<UpdateCommentResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<UpdateCommentResponse>> Handle(UpdateCommentRequest request, CancellationToken cancellationToken)
    {
        var comment = await _unitOfWork.CommentRepository.GetByIdAsync(request.Id);
        if (comment == null)
        {
            throw new Exception("Comment Not Found");   //-----------------------------
        }

        comment.Description = request.Description;

        _unitOfWork.CommentRepository.Update(comment);
        return new Result<UpdateCommentResponse>
        {
            Data = new UpdateCommentResponse { Description = request.Description },
            Errors = [],
            IsSuccess = true
        };
    }
}