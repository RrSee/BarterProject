using BarterProject.Application.CQRS.Comments.Commands.Requests;
using BarterProject.Application.CQRS.Comments.Commands.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Repository.Common;
using FluentValidation;
using MediatR;

namespace BarterProject.Application.CQRS.Comments.Handlers.CommandHandlers;

public class UpdateCommentHandler : IRequestHandler<UpdateCommentRequest, Result<UpdateCommentResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IValidator<UpdateCommentRequest> _validator;

    public UpdateCommentHandler(IUnitOfWork unitOfWork, IValidator<UpdateCommentRequest> validator)
    {
        _unitOfWork = unitOfWork;
        _validator = validator;
    }

    public async Task<Result<UpdateCommentResponse>> Handle(UpdateCommentRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new Result<UpdateCommentResponse>(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }

        var comment = await _unitOfWork.CommentRepository.GetByIdAsync(request.Id);
        if (comment == null)
        {
            return new Result<UpdateCommentResponse>(new List<string> { "Comment not found." });
        }

        comment.Description = request.Description;

        _unitOfWork.CommentRepository.Update(comment);
        await _unitOfWork.CommitAsync();

        return new Result<UpdateCommentResponse>
        {
            Data = new UpdateCommentResponse { Description = request.Description },
            Errors = new List<string>(),
            IsSuccess = true
        };
    }
}
