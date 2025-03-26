using AutoMapper;
using BarterProject.Application.CQRS.BarterRequests.Commands.Requests;
using BarterProject.Application.CQRS.BarterRequests.Commands.Responses;
using BarterProject.Common.GlobalResponses.Generics;
using BarterProject.Domain.Entites;
using BarterProject.Repository.Common;
using MediatR;

namespace BarterProject.Application.CQRS.BarterRequests.Handlers.CommandHandlers;

public class CreateBarterRequestHandler(IUnitOfWork unitOfWork, IMapper mapper) : IRequestHandler<CreateBarterRequestRequest, Result<CreateBarterRequestResponse>>
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    public async Task<Result<CreateBarterRequestResponse>> Handle(CreateBarterRequestRequest request, CancellationToken cancellationToken)
    {
        var newBarterRequest = _mapper.Map<BarterRequest>(request);

        await _unitOfWork.BarterRequestRepository.AddAsync(newBarterRequest);
        var response = _mapper.Map<CreateBarterRequestResponse>(newBarterRequest);

        return new Result<CreateBarterRequestResponse>
        {
            Data = response,
            Errors = [],
            IsSuccess = true
        };
    }
}
