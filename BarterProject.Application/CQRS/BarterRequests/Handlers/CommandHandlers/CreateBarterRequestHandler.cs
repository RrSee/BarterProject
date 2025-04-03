using AutoMapper;
using BarterProject.Application.CQRS.BarterRequests.Commands.Requests;
using BarterProject.Application.CQRS.BarterRequests.Commands.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Domain.Entites;
using BarterProject.Repository.Common;
using FluentValidation;
using MediatR;

namespace BarterProject.Application.CQRS.BarterRequests.Handlers.CommandHandlers;

public class CreateBarterRequestHandler : IRequestHandler<CreateBarterRequestRequest, Result<CreateBarterRequestResponse>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly IValidator<CreateBarterRequestRequest> _validator;

    public CreateBarterRequestHandler(IUnitOfWork unitOfWork, IMapper mapper, IValidator<CreateBarterRequestRequest> validator)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _validator = validator;
    }

    public async Task<Result<CreateBarterRequestResponse>> Handle(CreateBarterRequestRequest request, CancellationToken cancellationToken)
    {
        var validationResult = await _validator.ValidateAsync(request, cancellationToken);
        if (!validationResult.IsValid)
        {
            return new Result<CreateBarterRequestResponse>(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }
        var newBarterRequest = _mapper.Map<BarterRequest>(request);

        await _unitOfWork.BarterRequestRepository.AddAsync(newBarterRequest);
        await _unitOfWork.CommitAsync();

        var response = _mapper.Map<CreateBarterRequestResponse>(newBarterRequest);

        return new Result<CreateBarterRequestResponse>
        {
            Data = response,
            Errors = new List<string>(),
            IsSuccess = true
        };
    }
}
