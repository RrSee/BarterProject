using AutoMapper;
using BarterProject.Application.CQRS.Comments.Commands.Requests;
using BarterProject.Application.CQRS.Comments.Commands.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Domain.Entites;
using BarterProject.Repository.Common;
using FluentValidation;
using MediatR;

namespace BarterProject.Application.CQRS.Comments.Handlers.CommandHandlers;

public class CreateCommentRequestHandler : IRequestHandler<CreateCommentRequest, Result<CreateCommentResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateCommentRequest> _validator;

    public CreateCommentRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateCommentRequest> validator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Result<CreateCommentResponse>> Handle(CreateCommentRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new Result<CreateCommentResponse>(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }

        var newComment = _mapper.Map<Comment>(request);
        await _unitOfWork.CommentRepository.AddAsync(newComment);
        await _unitOfWork.CommitAsync();
        var response = _mapper.Map<CreateCommentResponse>(newComment);
        return new Result<CreateCommentResponse>
        {
            Data = response,
            Errors = new List<string>(),
            IsSuccess = true
        };
    }
}
