using BarterProject.Application.CQRS.Comments.Queries.Requests;
using BarterProject.Application.CQRS.Comments.Queries.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Repository.Common;
using MediatR;

namespace BarterProject.Application.CQRS.Comments.Handlers.QueryHandlers;

public class GetByIdCommentHandler(IUnitOfWork unitOfWork) : IRequestHandler<GetByIdCommentRequest, Result<GetByIdCommentResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;

    public async Task<Result<GetByIdCommentResponse>> Handle(GetByIdCommentRequest request, CancellationToken cancellationToken)
    {
        var comment = await _unitOfWork.CommentRepository.GetByIdAsync(request.Id);
        if(comment == null)
        {
            throw new Exception("Comment not found");
        }
        return new Result<GetByIdCommentResponse>
        {
            IsSuccess = true,
            Errors = [],
            Data = new GetByIdCommentResponse { Description = comment.Description, ItemId = comment.ItemId, UserId = comment.UserId }
        };
    }
}
