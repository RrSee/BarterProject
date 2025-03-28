using AutoMapper;
using BarterProject.Application.CQRS.Comments.Commands.Requests;
using BarterProject.Application.CQRS.Comments.Commands.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Domain.Entites;
using BarterProject.Repository.Common;
using MediatR;

namespace BarterProject.Application.CQRS.Comments.Handlers.CommandHandlers;

public class CreateCommentHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateCommentRequest, Result<CreateCommentResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<CreateCommentResponse>> Handle(CreateCommentRequest request, CancellationToken cancellationToken)
    {
        var newComment = _mapper.Map<Comment>(request);

        await _unitOfWork.CommentRepository.AddAsync(newComment);

        var response = _mapper.Map<CreateCommentResponse>(newComment);

        return new Result<CreateCommentResponse>
        {
            Data = response,
            IsSuccess = true,
            Errors = []
        };
    }
}
