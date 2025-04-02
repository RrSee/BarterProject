using AutoMapper;
using BarterProject.Application.CQRS.Comments.Queries.Requests;
using BarterProject.Application.CQRS.Comments.Queries.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Repository.Common;
using MediatR;

namespace BarterProject.Application.CQRS.Comments.Handlers.QueryHandlers;

public class GetByItemIdCommentHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<GetByItemIdCommentRequest, Result<List<GetByItemIdCommentResponse>>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;

    public async Task<Result<List<GetByItemIdCommentResponse>>> Handle(GetByItemIdCommentRequest request, CancellationToken cancellationToken)
    {
        var comments = await _unitOfWork.CommentRepository.GetByItemIdInitialDataAsync(request.ItemId);

        var response = comments.Select(x => _mapper.Map<GetByItemIdCommentResponse>(x)).ToList();

        return new Result<List<GetByItemIdCommentResponse>>() { Data = response, Errors = [], IsSuccess = true };
    }
}
