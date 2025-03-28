using AutoMapper;
using BarterProject.Application.CQRS.Comments.Queries.Requests;
using BarterProject.Application.CQRS.Comments.Queries.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Repository.Common;
using MediatR;

namespace BarterProject.Application.CQRS.Comments.Handlers.QueryHandlers;

public class GetAllCommentHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetAllCommentRequest, Result<List<GetAllCommentResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<List<GetAllCommentResponse>>> Handle(GetAllCommentRequest request, CancellationToken cancellationToken)
    {
        var comments = await _unitOfWork.CommentRepository.GetAllInitialDataAsync();

        var response = comments.Select(x => _mapper.Map<GetAllCommentResponse>(x)).ToList();
        return new Result<List<GetAllCommentResponse>>() { Data = response, IsSuccess = true, Errors = [] };
    }
}
